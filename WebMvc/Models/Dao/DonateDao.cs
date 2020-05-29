using Domain.EF;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebMvc.Configurations;
using WebMvc.Models.Enum;
using WebMvc.Models.ModelView;

namespace WebMvc.Models.Dao
{
    public class DonateDao
    {

        private int STATUS_ENDED = 3;
        private int ON_GOING = 2;
        

        private readonly IUnitOfWork _unitOfWork;

        public DonateDao(IUnitOfWork unit)
        {
            _unitOfWork = unit;
        }

        public void CheckStatusForDonate()
        {
            var data = _unitOfWork.DonateRepository.Get().Where(x => x.DonateStatus != STATUS_ENDED).ToList();
            foreach (var donate in data)
            {
                if (donate.StartDay >= DateTime.Now) donate.DonateStatus = ON_GOING;
                if (donate.EndDay < DateTime.Now) donate.DonateStatus = STATUS_ENDED;
                _unitOfWork.DonateRepository.Edit(donate);
                _unitOfWork.Commit();
            }
                
        }

        //get all list donate  
        public List<DonateDTO> GetAll()
        {
            var data = _unitOfWork.DonateRepository.Get();
            CheckStatusForDonate();
            return MapperProfile.MapperConfig().Map<List<Donate>, List<DonateDTO>>(data.ToList());
        }
        //
        public bool Create(DonateDTO donate)
        {
            try
            {
                donate.DonateStatus = donate.StartDay > DateTime.Now ? DonateStatus.UpComing : DonateStatus.OnGoing;
                donate.DonateDateCreate = DateTime.Now;
                donate.DonateHide = false;

                var data = MapperProfile.MapperConfig().Map<DonateDTO, Donate>(donate);
                _unitOfWork.DonateRepository.Create(data);
                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public DonateDTO GetById(int id)
        {
            var data = MapperProfile.MapperConfig().Map<DonateDTO>(_unitOfWork.DonateRepository.GetById(id));
            data.UserDonates = MapperProfile.MapperConfig()
                .Map<List<UserDonate>,List<UserDonateDTO>>(_unitOfWork.UserDonateRepository.Get().Where(x => x.DonateID == data.ID).ToList());
            return data;
        }

        public object GetByid(int id)
        {
            return MapperProfile.MapperConfig().Map<Donate, DonateDTO>(_unitOfWork.DonateRepository.GetById(id));
        }

        public bool Edit(DonateDTO donate)
        {
            try
            {
                donate.DonateStatus = donate.StartDay > DateTime.Now ? DonateStatus.UpComing : DonateStatus.OnGoing;
                donate.DonateDateCreate = _unitOfWork.DonateRepository.GetById(donate.ID).DonateDateCreate;
                donate.DonateStatus = donate.StartDay > DateTime.Now ? DonateStatus.UpComing : DonateStatus.OnGoing;
                var data = MapperProfile.MapperConfig().Map<DonateDTO, Donate>(donate);
                
                _unitOfWork.DonateRepository.Edit(data);
                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool HideDonate(int id)
        {
            var data = _unitOfWork.DonateRepository.GetById(id);
            if (data == null) return false;
            data.DonateHide = !data.DonateHide;
            _unitOfWork.DonateRepository.Edit(data);
            return _unitOfWork.Commit();
        }

        public List<DonateDTO> GetAllDonateNoHide()
        {
            var data = _unitOfWork.DonateRepository.Get().Where(x=>!x.DonateHide);
            CheckStatusForDonate();
            return MapperProfile.MapperConfig().Map<List<Donate>, List<DonateDTO>>(data.ToList());
        }

        public void AddUserDonate(OrderData order)
        {
            _unitOfWork.UserDonateRepository.Create(new UserDonate()
            {
                DonateID = order.DonateId,
                UserID = _unitOfWork.UserRepository.Get().SingleOrDefault(x=>x.UserMail == order.UserMail).ID,
                Money = order.Money,
                TypeCard = "VISA",
                DateCreate = DateTime.Now
            });

            var donateInfo= _unitOfWork.DonateRepository.GetById(order.DonateId);
            donateInfo.TotalMoney += order.Money;
            _unitOfWork.DonateRepository.Edit(donateInfo);
            _unitOfWork.Commit();
        }
    }
}
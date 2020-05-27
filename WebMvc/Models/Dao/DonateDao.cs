using Domain.EF;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using WebMvc.Configurations;
using WebMvc.Models.Enum;
using WebMvc.Models.ModelView;

namespace WebMvc.Models.Dao
{
    public class DonateDao
    {
        private IUnitOfWork _unitOfWork;

        public DonateDao(IUnitOfWork unit)
        {
            _unitOfWork = unit;
        }
        //get all list donate  
        public List<DonateDTO> GetAll()
        {
            var data = _unitOfWork.DonateRepository.Get();
            return MapperProfile.MapperConfig().Map<List<Donate>, List<DonateDTO>>(data.ToList());
        }
        //
        public bool Create(DonateDTO donate)
        {
            try
            {
                if (donate.StartDay > DateTime.Now)
                {
                    donate.DonateStatus = DonateStatus.UpComing;
                }
                else
                {
                    donate.DonateStatus = DonateStatus.OnGoing;
                }
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

        public object GetById(int id)
        {
            return MapperProfile.MapperConfig().Map<DonateDTO>(_unitOfWork.DonateRepository.GetById(id));
        }

        public object GetByid(int id)
        {
            return MapperProfile.MapperConfig().Map<Donate, DonateDTO>(_unitOfWork.DonateRepository.GetById(id));
        }

        public bool Edit(DonateDTO donate)
        {
            try
            {
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
    }
}
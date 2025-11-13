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
    /// <summary>
    /// Data Access Object for Donate.
    /// </summary>
    public class DonateDao
    {
        private const int STATUS_ENDED = 3;
        private const int ON_GOING = 2;
        private const int UP_COMING = 1;

        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="DonateDao"/> class.
        /// </summary>
        /// <param name="unit">The unit of work.</param>
        public DonateDao(IUnitOfWork unit)
        {
            _unitOfWork = unit;
        }

        /// <summary>
        /// Checks and updates the status of all donations.
        /// </summary>
        public void CheckStatusForDonate()
        {
            var data = _unitOfWork.DonateRepository.Get().Where(x => x.DonateStatus != STATUS_ENDED).ToList();
            foreach (var donate in data)
            {
                if (donate.StartDay > DateTime.Now) donate.DonateStatus = UP_COMING;
                if (donate.StartDay <= DateTime.Now && donate.EndDay >= DateTime.Now) donate.DonateStatus = ON_GOING;
                if (donate.EndDay < DateTime.Now) donate.DonateStatus = STATUS_ENDED;
                _unitOfWork.DonateRepository.Edit(donate);
                _unitOfWork.Commit();
            }
        }

        /// <summary>
        /// Gets all donations.
        /// </summary>
        /// <returns>A list of donations.</returns>
        public List<DonateDTO> GetAll()
        {
            var data = _unitOfWork.DonateRepository.Get();
            CheckStatusForDonate();
            return MapperProfile.MapperConfig().Map<List<Donate>, List<DonateDTO>>(data.ToList());
        }

        /// <summary>
        /// Creates a new donation.
        /// </summary>
        /// <param name="donate">The donation to create.</param>
        /// <returns>True if the creation was successful, otherwise false.</returns>
        public bool Create(DonateDTO donate)
        {
            try
            {
                donate.DonateStatus = donate.StartDay > DateTime.Now ? DonateStatus.Upcoming : DonateStatus.Ongoing;
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

        /// <summary>
        /// Gets a donation by its ID.
        /// </summary>
        /// <param name="id">The ID of the donation.</param>
        /// <returns>The donation.</returns>
        public DonateDTO GetById(int id)
        {
            var data = MapperProfile.MapperConfig().Map<DonateDTO>(_unitOfWork.DonateRepository.GetById(id));
            data.UserDonates = MapperProfile.MapperConfig()
                .Map<List<UserDonate>, List<UserDonateDTO>>(_unitOfWork.UserDonateRepository.Get().Where(x => x.DonateID == data.ID).ToList());
            return data;
        }

        /// <summary>
        /// Gets a donation by its ID.
        /// </summary>
        /// <param name="id">The ID of the donation.</param>
        /// <returns>The donation.</returns>
        public object GetByid(int id)
        {
            return MapperProfile.MapperConfig().Map<Donate, DonateDTO>(_unitOfWork.DonateRepository.GetById(id));
        }

        /// <summary>
        /// Edits an existing donation.
        /// </summary>
        /// <param name="donate">The donation to edit.</param>
        /// <returns>True if the edit was successful, otherwise false.</returns>
        public bool Edit(DonateDTO donate)
        {
            try
            {
                var dataDonate = _unitOfWork.DonateRepository.GetById(donate.ID);
                donate.DonateStatus = donate.StartDay > DateTime.Now ? DonateStatus.Upcoming : DonateStatus.Ongoing;
                donate.DonateDateCreate = dataDonate.DonateDateCreate;
                donate.TotalMoney = dataDonate.TotalMoney;
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

        /// <summary>
        /// Hides or unhides a donation.
        /// </summary>
        /// <param name="id">The ID of the donation.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        public bool HideDonate(int id)
        {
            var data = _unitOfWork.DonateRepository.GetById(id);
            if (data == null) return false;
            data.DonateHide = !data.DonateHide;
            _unitOfWork.DonateRepository.Edit(data);
            return _unitOfWork.Commit();
        }

        /// <summary>
        /// Gets all non-hidden donations.
        /// </summary>
        /// <returns>A list of non-hidden donations.</returns>
        public List<DonateDTO> GetAllDonateNoHide()
        {
            var data = _unitOfWork.DonateRepository.Get().Where(x => !x.DonateHide);
            CheckStatusForDonate();
            return MapperProfile.MapperConfig().Map<List<Donate>, List<DonateDTO>>(data.ToList());
        }

        /// <summary>
        /// Adds a user donation.
        /// </summary>
        /// <param name="order">The order data.</param>
        public void AddUserDonate(OrderData order)
        {
            _unitOfWork.UserDonateRepository.Create(new UserDonate()
            {
                DonateID = order.DonateId,
                UserID = _unitOfWork.UserRepository.Get().SingleOrDefault(x => x.UserMail == order.UserMail).ID,
                Money = order.Money,
                TypeCard = "VISA",
                DateCreate = DateTime.Now
            });

            var donateInfo = _unitOfWork.DonateRepository.GetById(order.DonateId);
            donateInfo.TotalMoney += order.Money;
            _unitOfWork.DonateRepository.Edit(donateInfo);
            var user = _unitOfWork.UserRepository.Get().SingleOrDefault(x => x.UserMail == order.UserMail);
            user.MoneyDonate += order.Money;
            _unitOfWork.UserRepository.Edit(user);
            _unitOfWork.Commit();
        }
    }
}
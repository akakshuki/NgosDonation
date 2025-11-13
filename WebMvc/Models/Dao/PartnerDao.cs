using Domain.EF;
using Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebMvc.Configurations;
using WebMvc.Models.ModelView;

namespace WebMvc.Models.Dao
{
    /// <summary>
    /// Data Access Object for Partner.
    /// </summary>
    public class PartnerDao
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string baseUrl = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerDao"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public PartnerDao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            baseUrl = "https://localhost:44315/FileImage/";
        }

        /// <summary>
        /// Gets all partners.
        /// </summary>
        /// <returns>A list of partners.</returns>
        public List<PartnerDTO> GetAll()
        {
            return MapperProfile.MapperConfig()
                .Map<List<Partner>, List<PartnerDTO>>(_unitOfWork.PartnerRepository.Get().ToList());
        }

        /// <summary>
        /// Gets a partner by its ID.
        /// </summary>
        /// <param name="id">The ID of the partner.</param>
        /// <returns>The partner.</returns>
        public PartnerDTO GetByid(int id)
        {
            var data = MapperProfile.MapperConfig().Map<Partner, PartnerDTO>(_unitOfWork.PartnerRepository.GetById(id));
            data.LinkImage = baseUrl + data.PartnerImage;
            return data;
        }

        /// <summary>
        /// Creates a new partner.
        /// </summary>
        /// <param name="partner">The partner to create.</param>
        /// <returns>True if the creation was successful, otherwise false.</returns>
        public bool Create(PartnerDTO partner)
        {
            try
            {
                var data = MapperProfile.MapperConfig().Map<PartnerDTO, Partner>(partner);
                _unitOfWork.PartnerRepository.Create(data);
                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Edits an existing partner.
        /// </summary>
        /// <param name="partner">The partner to edit.</param>
        /// <returns>True if the edit was successful, otherwise false.</returns>
        public bool Edit(PartnerDTO partner)
        {
            try
            {
                var data = MapperProfile.MapperConfig().Map<PartnerDTO, Partner>(partner);
                _unitOfWork.PartnerRepository.Edit(data);
                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
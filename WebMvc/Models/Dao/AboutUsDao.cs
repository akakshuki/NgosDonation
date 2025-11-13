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
    /// Data Access Object for About Us.
    /// </summary>
    public class AboutUsDao
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string baseUrl = "";

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutUsDao"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public AboutUsDao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            baseUrl = "https://localhost:44315/FileImage/";
        }

        /// <summary>
        /// Gets all About Us entries.
        /// </summary>
        /// <returns>A list of About Us entries.</returns>
        public List<AboutUsDTO> GetAll()
        {
            return MapperProfile.MapperConfig()
                .Map<List<AboutU>, List<AboutUsDTO>>(_unitOfWork.AboutUsRepository.Get().ToList());
        }

        /// <summary>
        /// Gets an About Us entry by its ID.
        /// </summary>
        /// <param name="id">The ID of the About Us entry.</param>
        /// <returns>The About Us entry.</returns>
        public AboutUsDTO GetByid(int id)
        {
            var data = MapperProfile.MapperConfig().Map<AboutU, AboutUsDTO>(_unitOfWork.AboutUsRepository.GetById(id));
            data.LinkImage = baseUrl + data.AboutImage;
            return data;
        }

        /// <summary>
        /// Creates a new About Us entry.
        /// </summary>
        /// <param name="aboutUs">The About Us entry to create.</param>
        /// <returns>True if the creation was successful, otherwise false.</returns>
        public bool Create(AboutUsDTO aboutUs)
        {
            try
            {
                var data = MapperProfile.MapperConfig().Map<AboutUsDTO, AboutU>(aboutUs);
                _unitOfWork.AboutUsRepository.Create(data);
                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Edits an existing About Us entry.
        /// </summary>
        /// <param name="aboutUs">The About Us entry to edit.</param>
        /// <returns>True if the edit was successful, otherwise false.</returns>
        public bool Edit(AboutUsDTO aboutUs)
        {
            try
            {
                var data = MapperProfile.MapperConfig().Map<AboutUsDTO, AboutU>(aboutUs);
                _unitOfWork.AboutUsRepository.Edit(data);
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
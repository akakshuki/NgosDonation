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
    /// Data Access Object for Program Image.
    /// </summary>
    public class ProgramImageDao
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramImageDao"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public ProgramImageDao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all program images.
        /// </summary>
        /// <returns>A list of program images.</returns>
        public List<ProgramImageDTO> GetAll()
        {
            return MapperProfile.MapperConfig()
                .Map<List<ProgramImage>, List<ProgramImageDTO>>(_unitOfWork.ProgramImageRepository.Get().ToList());
        }

        /// <summary>
        /// Gets a program image by its ID.
        /// </summary>
        /// <param name="id">The ID of the program image.</param>
        /// <returns>The program image.</returns>
        public ProgramImageDTO GetByid(int id)
        {
            return MapperProfile.MapperConfig().Map<ProgramImage, ProgramImageDTO>(_unitOfWork.ProgramImageRepository.GetById(id));
        }

        /// <summary>
        /// Creates a new program image.
        /// </summary>
        /// <param name="programImage">The program image to create.</param>
        /// <returns>True if the creation was successful, otherwise false.</returns>
        public bool Create(ProgramImageDTO programImage)
        {
            try
            {
                var data = MapperProfile.MapperConfig().Map<ProgramImageDTO, ProgramImage>(programImage);
                _unitOfWork.ProgramImageRepository.Create(data);
                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Gets the main image for a program.
        /// </summary>
        /// <param name="id">The ID of the program.</param>
        /// <returns>The main program image.</returns>
        public ProgramImageDTO GetImgMain(int id)
        {
            var data = MapperProfile.MapperConfig().Map<ProgramImage, ProgramImageDTO>(_unitOfWork.ProgramImageRepository.Get().SingleOrDefault(s => s.ProID == id && s.ImgMain == true));
            return data;
        }

        /// <summary>
        /// Toggles the main image status of a program image.
        /// </summary>
        /// <param name="id">The ID of the program image.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        public bool CheckImgMain(int id)
        {
            var data = _unitOfWork.ProgramImageRepository.GetById(id);
            if (data == null) return false;
            data.ImgMain = !data.ImgMain;
            _unitOfWork.ProgramImageRepository.Edit(data);
            return _unitOfWork.Commit();
        }

        /// <summary>
        /// Deletes a program image by its ID.
        /// </summary>
        /// <param name="id">The ID of the program image to delete.</param>
        /// <returns>True if the deletion was successful, otherwise false.</returns>
        public bool Delete(int id)
        {
            _unitOfWork.ProgramImageRepository.Delete(id);
            return _unitOfWork.Commit();
        }
    }
}
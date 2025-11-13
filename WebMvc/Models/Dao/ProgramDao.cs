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
    /// Data Access Object for Program.
    /// </summary>
    public class ProgramDao
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramDao"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public ProgramDao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all programs.
        /// </summary>
        /// <returns>A list of programs.</returns>
        public List<ProgramDTO> GetAll()
        {
            return MapperProfile.MapperConfig()
                .Map<List<Program>, List<ProgramDTO>>(_unitOfWork.ProgramRepository.Get().ToList());
        }

        /// <summary>
        /// Gets a program by its ID.
        /// </summary>
        /// <param name="id">The ID of the program.</param>
        /// <returns>The program.</returns>
        public ProgramDTO GetByid(int id)
        {
            return MapperProfile.MapperConfig().Map<Program, ProgramDTO>(_unitOfWork.ProgramRepository.GetById(id));
        }

        /// <summary>
        /// Creates a new program.
        /// </summary>
        /// <param name="program">The program to create.</param>
        /// <returns>True if the creation was successful, otherwise false.</returns>
        public bool Create(ProgramDTO program)
        {
            try
            {
                var data = MapperProfile.MapperConfig().Map<ProgramDTO, Program>(program);
                _unitOfWork.ProgramRepository.Create(data);
                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Deletes a program by its ID.
        /// </summary>
        /// <param name="id">The ID of the program to delete.</param>
        /// <returns>True if the deletion was successful, otherwise false.</returns>
        public bool Delete(int id)
        {
            _unitOfWork.ProgramRepository.Delete(id);
            return _unitOfWork.Commit();
        }

        /// <summary>
        /// Edits an existing program.
        /// </summary>
        /// <param name="program">The program to edit.</param>
        /// <returns>True if the edit was successful, otherwise false.</returns>
        public bool Edit(ProgramDTO program)
        {
            try
            {
                program.ProDateCreate = _unitOfWork.ProgramRepository.GetById(program.ID).ProDateCreate;
                var data = MapperProfile.MapperConfig().Map<ProgramDTO, Program>(program);
                _unitOfWork.ProgramRepository.Edit(data);
                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Hides or unhides a program.
        /// </summary>
        /// <param name="id">The ID of the program.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        public bool HideProgram(int id)
        {
            var data = _unitOfWork.ProgramRepository.GetById(id);
            if (data == null) return false;
            data.ProHide = !data.ProHide;
            _unitOfWork.ProgramRepository.Edit(data);
            return _unitOfWork.Commit();
        }
    }
}
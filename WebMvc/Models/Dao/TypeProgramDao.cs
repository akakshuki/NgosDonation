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
    /// Data Access Object for Type Program.
    /// </summary>
    public class TypeProgramDao
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeProgramDao"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public TypeProgramDao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all type programs.
        /// </summary>
        /// <returns>A list of type programs.</returns>
        public List<TypeProgramDTO> GetAll()
        {
            return MapperProfile.MapperConfig()
                .Map<List<TypeProgram>, List<TypeProgramDTO>>(_unitOfWork.TypeProgramRepository.Get().ToList());
        }

        /// <summary>
        /// Gets a type program by its ID.
        /// </summary>
        /// <param name="id">The ID of the type program.</param>
        /// <returns>The type program.</returns>
        public TypeProgramDTO GetByid(int id)
        {
            return MapperProfile.MapperConfig().Map<TypeProgram, TypeProgramDTO>(_unitOfWork.TypeProgramRepository.GetById(id));
        }

        /// <summary>
        /// Creates a new type program.
        /// </summary>
        /// <param name="typeProgram">The type program to create.</param>
        /// <returns>True if the creation was successful, otherwise false.</returns>
        public bool Create(TypeProgramDTO typeProgram)
        {
            try
            {
                var data = MapperProfile.MapperConfig().Map<TypeProgramDTO, TypeProgram>(typeProgram);
                _unitOfWork.TypeProgramRepository.Create(data);
                return _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        /// <summary>
        /// Deletes a type program by its ID.
        /// </summary>
        /// <param name="id">The ID of the type program to delete.</param>
        /// <returns>True if the deletion was successful, otherwise false.</returns>
        public bool Delete(int id)
        {
            _unitOfWork.TypeProgramRepository.Delete(id);
            return _unitOfWork.Commit();
        }

        /// <summary>
        /// Checks if a type program with the given name already exists.
        /// </summary>
        /// <param name="typeName">The name of the type program to check.</param>
        /// <returns>True if the type program exists, otherwise false.</returns>
        public bool CheckHaveExist(string typeName)
        {
            return _unitOfWork.TypeProgramRepository.Get()
                       .FirstOrDefault(x => x.TypeName.Equals(typeName)) != null;
        }

        /// <summary>
        /// Edits an existing type program.
        /// </summary>
        /// <param name="typeProgram">The type program to edit.</param>
        /// <returns>True if the edit was successful, otherwise false.</returns>
        public bool Edit(TypeProgramDTO typeProgram)
        {
            try
            {
                var data = MapperProfile.MapperConfig().Map<TypeProgram>(typeProgram);
                _unitOfWork.TypeProgramRepository.Edit(data);
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
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
    public class TypeProgramDao
    {
        private IUnitOfWork _unitOfWork;

        public TypeProgramDao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<TypeProgramDTO> GetAll()
        {
            return MapperProfile.MapperConfig()
                .Map<List<TypeProgram>, List<TypeProgramDTO>>(_unitOfWork.TypeProgramRepository.Get().ToList());
        }


        public TypeProgramDTO GetByid(int id)
        {
            return MapperProfile.MapperConfig().Map<TypeProgram, TypeProgramDTO>(_unitOfWork.TypeProgramRepository.GetById(id));
        }

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

        public bool Delete(int id)
        {
            _unitOfWork.TypeProgramRepository.Delete(id);
            return _unitOfWork.Commit();
        }

        public bool CheckHaveExist(string typeName)
        {
            return _unitOfWork.TypeProgramRepository.Get()
                       .FirstOrDefault(x => x.TypeName.Equals(typeName)) != null;
        }


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
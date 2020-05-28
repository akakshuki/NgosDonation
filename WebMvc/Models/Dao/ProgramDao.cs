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
    public class ProgramDao
    {
        private IUnitOfWork _unitOfWork;

        public ProgramDao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<ProgramDTO> GetAll()
        {
            return MapperProfile.MapperConfig()
                .Map<List<Program>, List<ProgramDTO>>(_unitOfWork.ProgramRepository.Get().ToList());
        }

        public ProgramDTO GetByid(int id)
        {
            return MapperProfile.MapperConfig().Map<Program, ProgramDTO>(_unitOfWork.ProgramRepository.GetById(id));
        }

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

        public bool Delete(int id)
        {
            _unitOfWork.ProgramRepository.Delete(id);
            return _unitOfWork.Commit();
        }

        public bool CheckHaveExist(string proName)
        {
            return _unitOfWork.ProgramRepository.Get()
                       .FirstOrDefault(x => x.ProName.Equals(proName)) != null;
        }


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

        public bool HideDonate(int id)
        {
            var data = _unitOfWork.ProgramRepository.GetById(id);
            if (data == null) return false;
            data.ProHide = !data.ProHide;
            _unitOfWork.ProgramRepository.Edit(data);
            return _unitOfWork.Commit();
        }
    }
}
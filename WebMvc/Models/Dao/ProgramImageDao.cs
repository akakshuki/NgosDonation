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
    public class ProgramImageDao
    {
        private IUnitOfWork _unitOfWork;

        public ProgramImageDao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<ProgramImageDTO> GetAll()
        {
            return MapperProfile.MapperConfig()
                .Map<List<ProgramImage>, List<ProgramImageDTO>>(_unitOfWork.ProgramImageRepository.Get().ToList());
        }

        public ProgramImageDTO GetByid(int id)
        {
            return MapperProfile.MapperConfig().Map<ProgramImage, ProgramImageDTO>(_unitOfWork.ProgramImageRepository.GetById(id));
        }

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

        public ProgramImageDTO GetImgMain(int id)
        {
            var data = MapperProfile.MapperConfig().Map<ProgramImage, ProgramImageDTO>(_unitOfWork.ProgramImageRepository.Get().SingleOrDefault(s => s.ProID == id && s.ImgMain == true));
            return data;
        }

        public bool CheckImgMain(int id)
        {
            var data = _unitOfWork.ProgramImageRepository.GetById(id);
            if (data == null) return false;
            data.ImgMain = !data.ImgMain;
            _unitOfWork.ProgramImageRepository.Edit(data);
            return _unitOfWork.Commit();
        }

        public bool Delete(int id)
        {
            _unitOfWork.ProgramImageRepository.Delete(id);
            return _unitOfWork.Commit();
        }
        
    }
}
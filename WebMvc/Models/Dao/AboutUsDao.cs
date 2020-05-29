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
    public class AboutUsDao
    {
        private IUnitOfWork _unitOfWork;
        private string baseUrl = "";

        public AboutUsDao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            baseUrl = "https://localhost:44315/FileImage/";
        }

        public List<AboutUsDTO> GetAll()
        {
            return MapperProfile.MapperConfig()
                .Map<List<AboutU>, List<AboutUsDTO>>(_unitOfWork.AboutUsRepository.Get().ToList());
        }

        public AboutUsDTO GetByid(int id)
        {
            var data = MapperProfile.MapperConfig().Map<AboutU, AboutUsDTO>(_unitOfWork.AboutUsRepository.GetById(id));
            data.LinkImage = baseUrl + data.AboutImage;
            return data;
        }

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
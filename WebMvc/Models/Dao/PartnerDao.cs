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
    public class PartnerDao
    {
        private IUnitOfWork _unitOfWork;
        private string baseUrl = "";

        public PartnerDao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            baseUrl = "https://localhost:44315/FileImage/";
        }

        public List<PartnerDTO> GetAll()
        {
            return MapperProfile.MapperConfig()
                .Map<List<Partner>, List<PartnerDTO>>(_unitOfWork.PartnerRepository.Get().ToList());
        }

        public PartnerDTO GetByid(int id)
        {
            var data = MapperProfile.MapperConfig().Map<Partner, PartnerDTO>(_unitOfWork.PartnerRepository.GetById(id));
            data.LinkImage = baseUrl + data.PartnerImage;
            return data;
        }

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
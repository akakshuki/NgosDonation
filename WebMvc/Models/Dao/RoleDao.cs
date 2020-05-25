using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.EF;
using Domain.Repository;
using WebMvc.Configurations;
using WebMvc.Models.ModelView;

namespace WebMvc.Models.Dao
{
    public class RoleDao
    {
        private  IUnitOfWork _unitOfWork;

        public RoleDao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public  List<RoleDTO> GetAllRole()
        {
            var data = _unitOfWork.RoleRepository.Get();

            return MapperProfile.MapperConfig().Map<List<Role>, List<RoleDTO>>(data.ToList());

        }
    }
}
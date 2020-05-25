using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Domain.EF;
using Domain.Repository;

namespace WebSite.Models.Dao
{
    public class RoleDao
    {
        private IUnitOfWork _unitOfWork;

        public RoleDao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async  Task<List<Role>> GetAll()
        {
            var data = await _unitOfWork.RoleRepository.Get();
            return data.ToList();
        }
    }
}
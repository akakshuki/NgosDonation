using Domain.EF;
using Domain.Repository;
using System.Collections.Generic;
using System.Linq;
using WebMvc.Configurations;
using WebMvc.Models.ModelView;

namespace WebMvc.Models.Dao
{
    /// <summary>
    /// Data Access Object for Role.
    /// </summary>
    public class RoleDao
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleDao"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public RoleDao(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all roles.
        /// </summary>
        /// <returns>A list of roles.</returns>
        public List<RoleDTO> GetAllRole()
        {
            var data = _unitOfWork.RoleRepository.Get();

            return MapperProfile.MapperConfig().Map<List<Role>, List<RoleDTO>>(data.ToList());
        }
    }
}
using System.Collections.Generic;

namespace WebMvc.Models.ModelView
{
    /// <summary>
    /// Represents the data transfer object for a role.
    /// </summary>
    public class RoleDTO
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the list of users in this role.
        /// </summary>
        public List<UserDTO> Users { get; set; }
    }
}
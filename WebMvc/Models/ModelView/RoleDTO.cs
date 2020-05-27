using System.Collections.Generic;

namespace WebMvc.Models.ModelView
{
    public class RoleDTO
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        public List<UserDTO> Users { get; set; }
    }
}
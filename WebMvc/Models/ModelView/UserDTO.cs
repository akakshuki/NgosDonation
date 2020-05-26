using System;

namespace WebMvc.Models.ModelView
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public bool UserGender { get; set; }
        public string UserMail { get; set; }
        public DateTime UserDOB { get; set; }
        public string UserPwd { get; set; }
        public DateTime UserDateCreate { get; set; }
        public bool UserActive { get; set; }
        public int RoleID { get; set; }
        public bool UserVolunteer { get; set; }
        public decimal MoneyDonate { get; set; }

        public RoleDTO Role { get; set; }
    }
}
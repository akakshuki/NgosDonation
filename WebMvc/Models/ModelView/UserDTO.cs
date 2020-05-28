using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebMvc.Models.ModelView
{
    public class UserDTO
    {
        public int ID { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Gender")]
        public bool UserGender { get; set; }
        [Display(Name = "Mail")]
        public string UserMail { get; set; }
        [Display(Name = "Birth Day")]
        public DateTime UserDOB { get; set; }
        public string UserPwd { get; set; }
        [Display(Name = "Date join")]
        public DateTime UserDateCreate { get; set; }
        public bool UserActive { get; set; }
        [Display(Name = "Role")]
        public int RoleID { get; set; }
        [Display(Name = "Volunteer")]
        public bool UserVolunteer { get; set; }
        [Display (Name = "Total money donate")]
        public decimal MoneyDonate { get; set; }

        public RoleDTO Role { get; set; }

        public List<UserDonateDTO> UserDonates { get; set; }
    }
}
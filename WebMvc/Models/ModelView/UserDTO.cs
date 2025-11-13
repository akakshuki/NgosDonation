using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebMvc.Models.ModelView
{
    /// <summary>
    /// Represents the data transfer object for a user.
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the gender of the user.
        /// </summary>
        [Display(Name = "Gender")]
        public bool UserGender { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        [Display(Name = "Mail")]
        public string UserMail { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the user.
        /// </summary>
        [Display(Name = "Birth Day")]
        public DateTime UserDOB { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string UserPwd { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the user.
        /// </summary>
        [Display(Name = "Date join")]
        public DateTime UserDateCreate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is active.
        /// </summary>
        public bool UserActive { get; set; }

        /// <summary>
        /// Gets or sets the role ID.
        /// </summary>
        [Display(Name = "Role")]
        public int RoleID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is a volunteer.
        /// </summary>
        [Display(Name = "Volunteer")]
        public bool UserVolunteer { get; set; }

        /// <summary>
        /// Gets or sets the total amount of money donated by the user.
        /// </summary>
        [Display(Name = "Total money donate")]
        public decimal MoneyDonate { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// </summary>
        public RoleDTO Role { get; set; }

        /// <summary>
        /// Gets or sets the list of user donations.
        /// </summary>
        public List<UserDonateDTO> UserDonates { get; set; }
    }

    /// <summary>
    /// Represents the data for a logged-in user.
    /// </summary>
    public class UserLogin
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string UserMail { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user is a volunteer.
        /// </summary>
        public bool UserVolunteer { get; set; }

        /// <summary>
        /// Gets or sets the role ID.
        /// </summary>
        public int RoleId { get; set; }
    }
}
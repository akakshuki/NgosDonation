using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WebMvc.Models.Enum;

namespace WebMvc.Models.ModelView
{
    /// <summary>
    /// Represents the data transfer object for a donation.
    /// </summary>
    public class DonateDTO
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the donation.
        /// </summary>
        [Required(ErrorMessage = "this is required"), Display(Name = "Donate Name")]
        public string DonateName { get; set; }

        /// <summary>
        /// Gets or sets the content of the donation.
        /// </summary>
        [Required(ErrorMessage = "this is required"), Display(Name = "Donate Content"), MaxLength(ErrorMessage = "Less than 200 character"), AllowHtml]
        public string DonateContent { get; set; }

        /// <summary>
        /// Gets or sets the start date of the donation.
        /// </summary>
        [Required(ErrorMessage = "This is required")]
        public DateTime StartDay { get; set; }

        /// <summary>
        /// Gets or sets the end date of the donation.
        /// </summary>
        [Required(ErrorMessage = "This is required")]
        public DateTime EndDay { get; set; }

        /// <summary>
        /// Gets or sets the status of the donation.
        /// </summary>
        public DonateStatus DonateStatus { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the donation is hidden.
        /// </summary>
        public bool DonateHide { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the donation.
        /// </summary>
        public DateTime DonateDateCreate { get; set; }

        /// <summary>
        /// Gets or sets the category ID.
        /// </summary>
        [Display(Name = "Category"), Required(ErrorMessage = "Must have a category")]
        public int CateID { get; set; }

        /// <summary>
        /// Gets or sets the total amount of money donated.
        /// </summary>
        public decimal TotalMoney { get; set; }

        /// <summary>
        /// Gets or sets the category of the donation.
        /// </summary>
        public CategoryDTO Category { get; set; }

        /// <summary>
        /// Gets or sets the list of user donations.
        /// </summary>
        public List<UserDonateDTO> UserDonates { get; set; }
    }

    /// <summary>
    /// Represents the data for a donation order.
    /// </summary>
    public class OrderData
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
        /// Gets or sets the ID of the donation.
        /// </summary>
        public int DonateId { get; set; }

        /// <summary>
        /// Gets or sets the name of the donation.
        /// </summary>
        public string DonateName { get; set; }

        /// <summary>
        /// Gets or sets the amount of money.
        /// </summary>
        public int Money { get; set; }
    }
}
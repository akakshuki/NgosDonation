using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WebMvc.Models.Enum;

namespace WebMvc.Models.ModelView
{
    public class DonateDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "this is required"), Display(Name = "Donate Name")]
        public string DonateName { get; set; }

        [Required(ErrorMessage = "this is required"), Display(Name = "Donate Content"), MaxLength(ErrorMessage = "Less than 200 character"), AllowHtml]
        public string DonateContent { get; set; }

        [Required(ErrorMessage = "This is required")]
        public DateTime StartDay { get; set; }

        [Required(ErrorMessage = "This is required")]
        public DateTime EndDay { get; set; }

        public DonateStatus DonateStatus { get; set; }
        public bool DonateHide { get; set; }
        public DateTime DonateDateCreate { get; set; }

        [Display(Name = "Category"), Required(ErrorMessage = "Must have a category")]
        public int CateID { get; set; }

        [Required(ErrorMessage = "this is required"), Display(Name = "Total Money")]
        public decimal TotalMoney { get; set; }

        public CategoryDTO Category { get; set; }
        public List<UserDonateDTO> UserDonates { get; set; }
    }
}
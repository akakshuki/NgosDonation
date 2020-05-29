using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMvc.Models.ModelView
{
    public class PartnerDTO
    {
        public int ID { get; set; }
        [Display(Name = "About Us")]
        public string PartnerName { get; set; }
        [Display(Name = "About Us")]
        public string PartnerImage { get; set; }
        public HttpPostedFileBase FileImage { get; set; }
        public string LinkImage { get; set; }
        [Display(Name = "About Us")]
        public string PartnerLink { get; set; }
        [Display(Name = "About Us")]
        public bool PartnerActive { get; set; }
    }
}
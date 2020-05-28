using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Models.ModelView
{
    public class AboutUsDTO
    {
        public int ID { get; set; }
        [Display(Name = "About Us")]
        public string AboutName { get; set; }
        [AllowHtml]
        [Display(Name = "About Content")]
        public string AboutContent { get; set; }
        [Display(Name = "Image")]
        public string AboutImage { get; set; }
        public HttpPostedFileBase FileImage { get; set; }
        public string LinkImage { get; set; }
        [Display(Name = "Hide")]
        public bool AboutHide { get; set; }
    }
}
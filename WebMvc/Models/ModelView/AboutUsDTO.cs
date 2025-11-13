using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Models.ModelView
{
    /// <summary>
    /// Represents the data transfer object for About Us.
    /// </summary>
    public class AboutUsDTO
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the about us section.
        /// </summary>
        [Display(Name = "About Us")]
        public string AboutName { get; set; }

        /// <summary>
        /// Gets or sets the content of the about us section.
        /// </summary>
        [AllowHtml]
        [Display(Name = "About Content")]
        public string AboutContent { get; set; }

        /// <summary>
        /// Gets or sets the image of the about us section.
        /// </summary>
        [Display(Name = "Image")]
        public string AboutImage { get; set; }

        /// <summary>
        /// Gets or sets the uploaded image file.
        /// </summary>
        public HttpPostedFileBase FileImage { get; set; }

        /// <summary>
        /// Gets or sets the link to the image.
        /// </summary>
        public string LinkImage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the about us section is hidden.
        /// </summary>
        [Display(Name = "Hide")]
        public bool AboutHide { get; set; }
    }
}
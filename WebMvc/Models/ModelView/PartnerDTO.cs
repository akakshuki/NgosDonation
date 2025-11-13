using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMvc.Models.ModelView
{
    /// <summary>
    /// Represents the data transfer object for a partner.
    /// </summary>
    public class PartnerDTO
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the partner.
        /// </summary>
        [Display(Name = "Partner Name")]
        public string PartnerName { get; set; }

        /// <summary>
        /// Gets or sets the image of the partner.
        /// </summary>
        [Display(Name = "Partner Image")]
        public string PartnerImage { get; set; }

        /// <summary>
        /// Gets or sets the uploaded image file.
        /// </summary>
        public HttpPostedFileBase FileImage { get; set; }

        /// <summary>
        /// Gets or sets the link to the image.
        /// </summary>
        public string LinkImage { get; set; }

        /// <summary>
        /// Gets or sets the link to the partner's website.
        /// </summary>
        [Display(Name = "Partner Link")]
        public string PartnerLink { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the partner is active.
        /// </summary>
        [Display(Name = "Active")]
        public bool PartnerActive { get; set; }
    }
}
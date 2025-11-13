using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvc.Models.ModelView
{
    /// <summary>
    /// Represents the data transfer object for a program image.
    /// </summary>
    public class ProgramImageDTO
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the program ID.
        /// </summary>
        public int ProID { get; set; }

        /// <summary>
        /// Gets or sets the image file name.
        /// </summary>
        public string ImgFileName { get; set; }

        /// <summary>
        /// Gets or sets the uploaded image file.
        /// </summary>
        public HttpPostedFileBase FileImage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this is the main image.
        /// </summary>
        public bool ImgMain { get; set; }

        /// <summary>
        /// Gets or sets the program.
        /// </summary>
        public virtual ProgramDTO Program { get; set; }
    }
}
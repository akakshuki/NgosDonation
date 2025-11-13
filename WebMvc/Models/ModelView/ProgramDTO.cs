using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Models.ModelView
{
    /// <summary>
    /// Represents the data transfer object for a program.
    /// </summary>
    public class ProgramDTO
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the program.
        /// </summary>
        [Display(Name = "Name Program")]
        public string ProName { get; set; }

        /// <summary>
        /// Gets or sets the content of the program.
        /// </summary>
        [AllowHtml]
        [Display(Name = "Program Content")]
        public string ProContent { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the program.
        /// </summary>
        [Display(Name = "Date Create")]
        public System.DateTime ProDateCreate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the program is hidden.
        /// </summary>
        public bool ProHide { get; set; }

        /// <summary>
        /// Gets or sets the type ID.
        /// </summary>
        public int TypeID { get; set; }

        /// <summary>
        /// Gets or sets the type of the program.
        /// </summary>
        public virtual TypeProgramDTO TypeProgram { get; set; }

        /// <summary>
        /// Gets or sets the collection of program images.
        /// </summary>
        public virtual ICollection<ProgramImageDTO> ProgramImages { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMvc.Models.ModelView
{
    /// <summary>
    /// Represents the data transfer object for a type of program.
    /// </summary>
    public class TypeProgramDTO
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the type.
        /// </summary>
        [Display(Name = "Type Name"), Required(ErrorMessage = "This is required")]
        public string TypeName { get; set; }

        /// <summary>
        /// Gets or sets the collection of programs of this type.
        /// </summary>
        public virtual ICollection<ProgramDTO> Programs { get; set; }
    }
}
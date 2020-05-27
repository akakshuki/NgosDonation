using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebMvc.Models.ModelView
{
    public class TypeProgramDTO
    {
        public int ID { get; set; }
        [Display(Name = "Type Name"), Required(ErrorMessage = "This is required")]
        public string TypeName { get; set; }
        public virtual ICollection<ProgramDTO> Programs { get; set; }
    }
}
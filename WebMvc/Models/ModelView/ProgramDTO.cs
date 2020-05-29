using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Models.ModelView
{
    public class ProgramDTO
    {
        public int ID { get; set; }
        [Display(Name = "Name Program")]
        public string ProName { get; set; }
        [AllowHtml]
        [Display(Name = "Program Content")]
        public string ProContent { get; set; }
        [Display(Name = "Date Create")]
        public System.DateTime ProDateCreate { get; set; }
        public bool ProHide { get; set; }
        public int TypeID { get; set; }
        public virtual TypeProgramDTO TypeProgram { get; set; }
        public virtual ICollection<ProgramImageDTO> ProgramImages { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMvc.Models.ModelView
{
    public class ProgramDTO
    {
        public int ID { get; set; }
        public string ProName { get; set; }
        [AllowHtml]
        public string ProContent { get; set; }
        public System.DateTime ProDateCreate { get; set; }
        public bool ProHide { get; set; }
        public int TypeID { get; set; }

        public virtual TypeProgramDTO TypeProgram { get; set; }
       
        public virtual ICollection<ProgramImageDTO> ProgramImages { get; set; }
    }
}
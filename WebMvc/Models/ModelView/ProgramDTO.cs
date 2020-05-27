using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvc.Models.ModelView
{
    public class ProgramDTO
    {
        public int ID { get; set; }
        public string ProName { get; set; }
        public string ProContent { get; set; }
        public System.DateTime ProDateCreate { get; set; }
        public bool ProHide { get; set; }
        public int TypeID { get; set; }

        public virtual TypeProgramDTO TypeProgramDto { get; set; }
       
        public virtual ICollection<ProgramImageDTO> ProgramImages { get; set; }
    }
}
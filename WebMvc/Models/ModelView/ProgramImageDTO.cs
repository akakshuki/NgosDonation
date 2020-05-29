using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvc.Models.ModelView
{
    public class ProgramImageDTO
    {
        public int ID { get; set; }
        public int ProID { get; set; }
        public string ImgFileName { get; set; }
        public HttpPostedFileBase FileImage { get; set; }
        public bool ImgMain { get; set; }

        public virtual ProgramDTO Program { get; set; }
    }
}
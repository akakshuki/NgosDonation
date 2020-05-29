using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvc.Models.ModelView
{
    public class UserQuestionDTO
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string UserMail { get; set; }
        public string Title { get; set; }
        public string QuesContent { get; set; }
        public System.DateTime QuesDateCreate { get; set; }
        public bool QuesNew { get; set; }
        public string AnswerContent { get; set; }
        public Nullable<System.DateTime> AnswerDateCreate { get; set; }
    }
}
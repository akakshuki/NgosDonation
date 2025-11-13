using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvc.Models.ModelView
{
    /// <summary>
    /// Represents the data transfer object for a user question.
    /// </summary>
    public class UserQuestionDTO
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email of the user.
        /// </summary>
        public string UserMail { get; set; }

        /// <summary>
        /// Gets or sets the title of the question.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the content of the question.
        /// </summary>
        public string QuesContent { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the question.
        /// </summary>
        public System.DateTime QuesDateCreate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the question is new.
        /// </summary>
        public bool QuesNew { get; set; }

        /// <summary>
        /// Gets or sets the content of the answer.
        /// </summary>
        public string AnswerContent { get; set; }

        /// <summary>
        /// Gets or sets the creation date of the answer.
        /// </summary>
        public Nullable<System.DateTime> AnswerDateCreate { get; set; }
    }
}
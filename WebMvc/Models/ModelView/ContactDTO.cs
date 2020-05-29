using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvc.Models.ModelView
{
    [Serializable]
    public class ContactDTO
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
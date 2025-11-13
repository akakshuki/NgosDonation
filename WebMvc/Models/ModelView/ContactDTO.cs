using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMvc.Models.ModelView
{
    /// <summary>
    /// Represents the data transfer object for contact information.
    /// </summary>
    [Serializable]
    public class ContactDTO
    {
        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the physical address.
        /// </summary>
        public string Address { get; set; }
    }
}
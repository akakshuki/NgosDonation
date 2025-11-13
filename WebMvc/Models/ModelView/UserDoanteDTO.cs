namespace WebMvc.Models.ModelView
{
    /// <summary>
    /// Represents the data transfer object for a user donation.
    /// </summary>
    public class UserDonateDTO
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Gets or sets the donation ID.
        /// </summary>
        public int DonateID { get; set; }

        /// <summary>
        /// Gets or sets the type of card used for the donation.
        /// </summary>
        public string TypeCard { get; set; }

        /// <summary>
        /// Gets or sets the amount of money donated.
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// Gets or sets the date of the donation.
        /// </summary>
        public System.DateTime DateCreate { get; set; }

        /// <summary>
        /// Gets or sets the user who made the donation.
        /// </summary>
        public UserDTO User { get; set; }

        /// <summary>
        /// Gets or sets the donation.
        /// </summary>
        public DonateDTO Donate { get; set; }
    }
}
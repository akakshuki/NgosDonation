namespace WebMvc.Models.Enum
{
    /// <summary>
    /// Represents the status of a donation.
    /// </summary>
    public enum DonateStatus
    {
        /// <summary>
        /// The donation is upcoming.
        /// </summary>
        Upcoming = 1,

        /// <summary>
        /// The donation is ongoing.
        /// </summary>
        Ongoing = 2,

        /// <summary>
        /// The donation has ended.
        /// </summary>
        Ended = 3
    }
}
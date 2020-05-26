namespace WebMvc.Models.ModelView
{
    public class UserDonateDTO
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int DonateID { get; set; }
        public string TypeCard { get; set; }
        public decimal Money { get; set; }
        public System.DateTime DateCreate { get; set; }

        public UserDTO User { get; set; }
        public DonateDTO Donate { get; set; }
    }
}
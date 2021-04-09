namespace TravelAgencyListImplement.Models
{
    public class Guide
    {
        public int Id { get; set; }

        public string GuideName { get; set; }

        public string Passport { set; get; }

        public int TripId { get; set; }

    }
}

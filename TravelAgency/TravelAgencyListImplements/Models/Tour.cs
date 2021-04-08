using System;


namespace TravelAgencyListImplement.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string PlaceOfResidence { get; set; }

        public string PlaceOfDeparture { get; set; }

        public DateTime DateOfDeparture { get; set; }
    }
}

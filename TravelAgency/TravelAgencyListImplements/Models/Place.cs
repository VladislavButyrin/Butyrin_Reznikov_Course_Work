using System.Collections.Generic;

namespace TravelAgencyListImplement.Models
{
    public class Place
    {
        public int Id { get; set; }

        public string PlaceName { get; set; }

        public string Adress { get; set; }

        public Dictionary<int, string> Trips { get; set; }

        public int GroupId { get; set; }

    }
}

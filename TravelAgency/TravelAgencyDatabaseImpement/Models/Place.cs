using System.Collections.Generic;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Place
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public List<Trip> Trips { get; set; }

        public Group Group { get; set; }

    }
}

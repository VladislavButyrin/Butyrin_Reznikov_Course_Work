using System.Collections.Generic;

namespace TravelAgencyListImplement.Models
{
    public class Place
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        public Dictionary<int,int> Trips { get; set; }

        public int GroupId { get; set; }

    }
}

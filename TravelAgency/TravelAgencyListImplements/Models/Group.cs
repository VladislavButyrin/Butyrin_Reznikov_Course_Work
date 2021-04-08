using System.Collections.Generic;

namespace TravelAgencyListImplement.Models
{
    public class Group
    {
        public int Id { get; set; }

        public Dictionary<int, int> Tours { get; set; }

    }
}

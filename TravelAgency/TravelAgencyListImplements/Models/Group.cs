using System.Collections.Generic;

namespace TravelAgencyListImplement.Models
{
    public class Group
    {
        public int Id { get; set; }

        public string GroupName { set; get; }

        public Dictionary<int, string> Tours { get; set; }

        public Dictionary<int, string> Tourists { set; get; }
    }
}

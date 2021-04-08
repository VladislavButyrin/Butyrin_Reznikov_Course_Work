using System.Collections.Generic;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Group
    {
        public int Id { get; set; }

        public List<Tour> Tours { get; set; }

        public int PeopleQuantity { get; set; }

    }
}

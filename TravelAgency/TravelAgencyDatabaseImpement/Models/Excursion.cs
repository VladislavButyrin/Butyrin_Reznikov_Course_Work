using System;
using System.Collections.Generic;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Excursion
    {
        public int Id { get; set; }

        public string ExcursionName { get; set; }

        public string Place { get; set; }

        public DateTime Date { get; set; }

        public Guide Guide { get; set; }

        public List<Tour> Tours { get; set; }

    }
}

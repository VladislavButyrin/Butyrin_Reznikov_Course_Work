using System;
using System.Collections.Generic;

namespace TravelAgencyListImplement.Models
{
    public class Excursion
    {
        public int Id { get; set; }

        public string ExcursionName { get; set; }

        //public string Place { get; set; }

        public DateTime Date { get; set; }

        public int GuideId { get; set; }

        public Dictionary<int, string> Tours { get; set; }

    }
}

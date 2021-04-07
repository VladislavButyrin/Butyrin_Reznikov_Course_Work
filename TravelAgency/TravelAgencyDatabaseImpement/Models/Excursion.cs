using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Excursion
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Place { get; set; }

        public DateTime Date { get; set; }

        public Guide Guide { get; set; }

        public List<Tour> Tours { get; set; }
       
    }
}

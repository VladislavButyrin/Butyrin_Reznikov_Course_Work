using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Trip
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
       
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TravelAgencyDatabaseImplement.Models
{
    public class Tour
    {
        public int Id { get; set; }
        public string TourName { get; set; }

        public string PlaceOfResidence { get; set; }

        public string PlaceOfDeparture { get; set; }    
        
        public DateTime DateOfDeparture { get; set; }
    }
}

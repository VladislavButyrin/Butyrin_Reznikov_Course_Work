using System;
using System.ComponentModel.DataAnnotations;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Trip
    {
        public int Id { get; set; }
        [Required]
        public string TripName { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public int GuideId { set; get; }

        public int PlaceId { set; get; }
    }
}

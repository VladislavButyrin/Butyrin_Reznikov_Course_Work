using System;
using System.ComponentModel.DataAnnotations;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Tour
    {
        public int Id { get; set; }
        [Required]
        public string TourName { get; set; }
        [Required]
        public string PlaceOfResidence { get; set; }
        [Required]
        public string PlaceOfDeparture { get; set; }
        [Required]
        public DateTime DateOfDeparture { get; set; }

        public int ExcursionId { set; get; }

        public int GroupId { set; get; }
    }
}

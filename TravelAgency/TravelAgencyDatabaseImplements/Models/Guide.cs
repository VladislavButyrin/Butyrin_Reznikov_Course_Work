using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyDatabaseImplements.Models
{
    public class Guide
    {
        public int Id { get; set; }
        [Required]
        public string GuideName { get; set; }
        [Required]
        public int Cost { get; set; }
        [ForeignKey("GuideId")]
        public virtual List<TripGuide> TripsGuides { get; set; }
    }
}
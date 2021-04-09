using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyDatabaseImplement.Models
{
    public class Place
    {
        public int Id { get; set; }
        [Required]
        public string PlaceName { get; set; }
        [Required]
        public string Adress { get; set; }
        [ForeignKey("PlaceId")]
        public List<Trip> Trips { get; set; }
        [ForeignKey("PlaceId")]
        public Group Group { get; set; }

    }
}

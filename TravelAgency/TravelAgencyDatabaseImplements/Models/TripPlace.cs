using System.ComponentModel.DataAnnotations;

namespace TravelAgencyDatabaseImplements.Models
{
    public class TripPlace
    {
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public int TripId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Trip Trip { get; set; }
        public virtual Place Place { get; set; }
    }
}
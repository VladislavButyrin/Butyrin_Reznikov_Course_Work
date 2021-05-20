using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyEntitiesImplements.Modules
{
    /// <summary>
    /// Медикамент, на его основе создаются лекарства
    /// </summary>
    public class Trip
    {
        public int Id { get; set; }
        [Required]
        public string TripName { get; set; }
        [ForeignKey("TripId")]
        public virtual List<TripGuide> TripsGuides { get; set; }
        [ForeignKey("TripId")]
        public virtual List<TripPlace> TripsPlaces { get; set; }
    }
}

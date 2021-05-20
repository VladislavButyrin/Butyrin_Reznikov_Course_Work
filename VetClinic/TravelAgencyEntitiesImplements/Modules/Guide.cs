using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyEntitiesImplements.Modules
{
    /// <summary>
    /// Сущность гида
    /// </summary>
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

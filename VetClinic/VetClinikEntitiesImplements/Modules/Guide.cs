using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetClinikEntitiesImplements.Modules
{
    /// <summary>
    /// Лекарство, создаётся на основе медикаментов
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

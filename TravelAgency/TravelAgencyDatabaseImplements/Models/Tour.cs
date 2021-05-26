using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgencyDatabaseImplements.Models
{
    public class Tour
    {
        public int? Id { get; set; }
        [Required]
        public string TourName { get; set; }
        [ForeignKey("TourId")]
        public virtual List<TourExcursion> ToursExcursions { get; set; }
    }
}
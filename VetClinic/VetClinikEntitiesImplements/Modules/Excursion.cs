using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetClinikEntitiesImplements.Modules
{
    public class Excursion
    {
        public int Id { get; set; }
        [Required]
        public decimal Sum { get; set; }
        public DateTime DatePayment { get; set; }
        [ForeignKey("ExcursionId")]
        public virtual List<GuideExcursion> GuidesExcursions { get; set; }
        [ForeignKey("ExcursionId")]
        public virtual List<TourExcursion> ToursExcursions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetClinikEntitiesImplements.Modules
{
    /// <summary>
    /// Сущность посещения врача
    /// </summary>
    public class Visit
    {
        public int Id { get; set; }
        [Required]
        public DateTime DateVisit { get; set; }
        [ForeignKey("VisitId")]
        public virtual List<AnimalVisit> AnimalsVisits { get; set; }
        [ForeignKey("VisitId")]
        public virtual List<VisitService> VisitServices { get; set; }
    }
}

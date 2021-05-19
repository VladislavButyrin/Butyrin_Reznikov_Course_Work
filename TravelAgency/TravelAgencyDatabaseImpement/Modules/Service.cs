using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetClinikEntitiesImplements.Modules
{
    /// <summary>
    /// Сущность услуги, которую оказывает ветеринарная клиника
    /// </summary>
    public class Service
    {
        public int Id { get; set; }
        [Required]
        public int DoctorId { get; set; }
        [Required]
        public string ServiceName { get; set; }
        [Required]
        public int Cost { get; set; }
        [ForeignKey("ServiceId")]
        public virtual List<MedicationService> MedicationsServices { get; set; }
        [ForeignKey("ServiceId")]
        public virtual List<VisitService> VisitsServices { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}

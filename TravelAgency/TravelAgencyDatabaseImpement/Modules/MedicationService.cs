﻿using System.ComponentModel.DataAnnotations;

namespace VetClinikEntitiesImplements.Modules
{
    /// <summary>
    /// Сущность связи медикамента и услуги
    /// </summary>
    public class MedicationService
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int MedicationId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Medication Medication { get; set; }
        public virtual Service Service { get; set; }
    }
}

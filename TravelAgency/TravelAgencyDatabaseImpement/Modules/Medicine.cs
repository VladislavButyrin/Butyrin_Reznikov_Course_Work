using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VetClinikEntitiesImplements.Modules
{
    /// <summary>
    /// Лекарство, создаётся на основе медикаментов
    /// </summary>
    public class Medicine
    {
        public int Id { get; set; }
        [Required]
        public string MedicineName { get; set; }
        [Required]
        public int Cost { get; set; }
        [ForeignKey("MedicineId")]
        public virtual List<MedicationMedicine> MedicationsMedicines { get; set; }
    }
}

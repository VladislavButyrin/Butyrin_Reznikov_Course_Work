using System.ComponentModel.DataAnnotations;

namespace VetClinikEntitiesImplements.Modules
{
    /// <summary>
    /// Сущность связи медикамента и лекарства
    /// </summary>
    public class MedicationMedicine
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public int MedicationId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Medication Medication { get; set; }
        public virtual Medicine Medicine { get; set; }
    }
}

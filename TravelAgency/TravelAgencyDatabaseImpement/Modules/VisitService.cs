using System.ComponentModel.DataAnnotations;

namespace VetClinikEntitiesImplements.Modules
{
    /// <summary>
    /// Сущность связи посещения врача и услуги
    /// </summary>
    public class VisitService
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int VisitId { get; set; }
        [Required]
        public virtual Visit Visit { get; set; }
        public virtual Service Service { get; set; }
    }
}

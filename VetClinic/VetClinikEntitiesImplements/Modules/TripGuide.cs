using System.ComponentModel.DataAnnotations;

namespace VetClinikEntitiesImplements.Modules
{
    /// <summary>
    /// Сущность связи медикамента и лекарства
    /// </summary>
    public class TripGuide
    {
        public int Id { get; set; }
        public int GuideId { get; set; }
        public int TripId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Trip Trip { get; set; }
        public virtual Guide Guide { get; set; }
    }
}

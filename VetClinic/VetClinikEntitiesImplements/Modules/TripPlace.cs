using System.ComponentModel.DataAnnotations;

namespace VetClinikEntitiesImplements.Modules
{
    /// <summary>
    /// Сущность связи медикамента и услуги
    /// </summary>
    public class TripPlace
    {
        public int Id { get; set; }
        public int PlaceId { get; set; }
        public int TripId { get; set; }
        [Required]
        public int Count { get; set; }
        public virtual Trip Trip { get; set; }
        public virtual Place Place { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace VetClinikEntitiesImplements.Modules
{
    public class TourGroup
    {
        public int Id { get; set; }
        [Required]
        public int TourId { get; set; }
        [Required]
        public int GroupId { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual Group Group { get; set; }
    }
}

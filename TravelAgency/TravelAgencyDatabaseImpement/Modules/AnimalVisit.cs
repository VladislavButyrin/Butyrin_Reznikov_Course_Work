using System.ComponentModel.DataAnnotations;

namespace VetClinikEntitiesImplements.Modules
{
    public class AnimalVisit
    {
        public int Id { get; set; }
        [Required]
        public int AnimalId { get; set; }
        [Required]
        public int VisitId { get; set; }
        public virtual Animal Animal { get; set; }
        public virtual Visit Visit { get; set; }
    }
}

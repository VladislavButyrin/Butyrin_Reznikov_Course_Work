using VetClinikEntitiesImplements.Modules;

namespace VetClinikEntitiesImplements.Modules
{
    public class GuideExcursion
    {
        public int Id { get; set; }
        public int GuideId { get; set; }
        public int ExcursionId { get; set; }
        public int Count { get; set; }
        public virtual Guide Guide { get; set; }
        public virtual Excursion Excursion { get; set; }
    }
}

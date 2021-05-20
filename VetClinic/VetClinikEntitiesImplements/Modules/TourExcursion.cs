namespace VetClinikEntitiesImplements.Modules
{
    public class TourExcursion
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public int ExcursionId { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual Excursion Excursion { get; set; }
    }
}

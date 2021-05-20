using Microsoft.EntityFrameworkCore;
using TravelAgencyEntitiesImplements.Modules;

namespace TravelAgencyEntitiesImplements
{
    class TravelAgencyDataBase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LawFirmDatabaseReznLab4E;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Trip> Trips { set; get; }
        public virtual DbSet<Guide> Guides { set; get; }
        public virtual DbSet<TripGuide> TripsGuides { set; get; }
        public virtual DbSet<Place> Places { set; get; }
        public virtual DbSet<TripPlace> TripsPlaces { set; get; }
        public virtual DbSet<Group> Groups { set; get; }
        public virtual DbSet<GroupPlace> GroupsPlaces { set; get; }
        public virtual DbSet<Organizator> Organizators { set; get; }
        public virtual DbSet<Tour> Tours { set; get; }
        public virtual DbSet<TourGroup> ToursGroups { set; get; }
        public virtual DbSet<Excursion> Excursions { set; get; }
        public virtual DbSet<GuideExcursion> GuidesExcursions { set; get; }
        public virtual DbSet<TourExcursion> ToursExcursions { set; get; }
        public virtual DbSet<MessageInfo> MessageInfos { set; get; }
    }
}

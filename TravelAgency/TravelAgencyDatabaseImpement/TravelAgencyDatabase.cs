using TravelAgencyDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;

namespace TravelAgencyDatabaseImpement
{
    public class TravelAgencyDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Tour> Tours { set; get; }
        public virtual DbSet<Excursion> Excursions { set; get; }
        public virtual DbSet<Group> Groups { set; get; }
        public virtual DbSet<Guide> Guides { set; get; }
        public virtual DbSet<Place> Places { set; get; }
        public virtual DbSet<Trip> Trips { set; get; }
    }
}

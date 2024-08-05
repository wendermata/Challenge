using Domain.Entities;
using Infrastructure.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EntityFramework
{
    public class ChallengeDbContext : DbContext
    {
        public DbSet<Motorcycle> Motorcycle => Set<Motorcycle>();

        public ChallengeDbContext(DbContextOptions<ChallengeDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            AddMotorcycleModel(ref builder);
            AddRenterModel(ref builder);
            AddRentalModel(ref builder);
        }

        private static void AddMotorcycleModel(ref ModelBuilder builder)
        {
            builder.Entity<Motorcycle>().ToTable("motorcycles");
            builder.ApplyConfiguration(new MotorcycleConfiguration());
        }

        private static void AddRenterModel(ref ModelBuilder builder)
        {
            builder.Entity<Renter>().ToTable("renters");
            builder.ApplyConfiguration(new RenterConfiguration());
        }

        private static void AddRentalModel(ref ModelBuilder builder)
        {
            builder.Entity<Rental>().ToTable("rentals");
            builder.ApplyConfiguration(new RentalConfiguration());
        }
    }
}

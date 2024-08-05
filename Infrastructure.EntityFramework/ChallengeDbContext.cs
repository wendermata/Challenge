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
            builder.Entity<Motorcycle>().ToTable("motorcycles");
            builder.ApplyConfiguration(new MotorcycleConfiguration());
        }
    }
}

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Configurations
{
    public class RenterConfiguration : IEntityTypeConfiguration<Renter>
    {
        public void Configure(EntityTypeBuilder<Renter> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasAlternateKey(x => x.Document);
            builder.HasAlternateKey(x => x.LicenseNumber);
        }
    }
}

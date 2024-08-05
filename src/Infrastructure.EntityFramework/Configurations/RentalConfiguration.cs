using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFramework.Configurations
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne<Renter>()
                .WithMany()
                .HasForeignKey(x => x.RenterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Motorcycle>()
                .WithMany()
                .HasForeignKey(x => x.MotorcycleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

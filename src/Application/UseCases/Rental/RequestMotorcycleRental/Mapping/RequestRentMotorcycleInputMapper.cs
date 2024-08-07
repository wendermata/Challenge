using Application.UseCases.Rental.RequestMotorcycleRental.Inputs;
using RentalDomain = Domain.Entities.Rental;

namespace Application.UseCases.Rental.RequestMotorcycleRental.Mapping
{
    public static class RequestRentMotorcycleInputMapper
    {
        public static RentalDomain MapToDomain(this RequestMotorcycleRentalInput request)
        {
            if (request is null)
                return null;

            return new RentalDomain(Guid.NewGuid(),
                request.RenterId,
                request.MotorcycleId,
                request.PlanType,
                request.StartDate);
        }
    }
}

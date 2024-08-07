using Application.UseCases.RequestRentMotorcycle.Inputs;
using Domain.Entities;

namespace Application.UseCases.RequestRentMotorcycle.Mapping
{
    public static class RequestRentMotorcycleInputMapper
    {
        public static Rental MapToDomain(this RequestRentMotorcycleInput request)
        {
            if (request is null)
                return null;

            return new Rental(Guid.NewGuid(),
                request.RenterId,
                request.MotorcycleId,
                request.PlanType,
                request.StartDate);
        }
    }
}

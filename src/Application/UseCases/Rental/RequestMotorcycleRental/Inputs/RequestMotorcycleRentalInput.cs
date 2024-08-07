using Application.Common;
using Domain.Enums;
using MediatR;

namespace Application.UseCases.Rental.RequestMotorcycleRental.Inputs
{
    public class RequestMotorcycleRentalInput : IRequest<Output>
    {
        public Guid RenterId { get; set; }
        public Guid MotorcycleId { get; set; }
        public DateTime StartDate { get; set; }
        public PlanType PlanType { get; set; }
    }
}
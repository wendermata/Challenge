using Application.UseCases.Rental.RequestMotorcycleRentalClosure.Outputs;
using MediatR;

namespace Application.UseCases.Rental.RequestMotorcycleRentalClosure.Inputs
{
    public class RequestMotorcycleRentalClosureInput : IRequest<RequestMotorcycleRentalClosureOutput>
    {
        public Guid RentalId { get; set; }
        public DateTime ClosureDate { get; set; }
    }
}
using Application.UseCases.Rental.RequestMotorcycleRentalClosure.Inputs;
using Application.UseCases.Rental.RequestMotorcycleRentalClosure.Outputs;
using MediatR;

namespace Application.UseCases.Rental.RequestMotorcycleRentalClosure
{
    public interface IRequestMotorcycleRentalClosureUseCase : IRequestHandler<RequestMotorcycleRentalClosureInput, RequestMotorcycleRentalClosureOutput>
    {
    }
}

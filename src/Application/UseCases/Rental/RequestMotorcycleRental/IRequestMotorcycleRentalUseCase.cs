using Application.Common;
using Application.UseCases.Rental.RequestMotorcycleRental.Inputs;
using MediatR;

namespace Application.UseCases.Rental.RequestRentMotorcycle
{
    public interface IRequestMotorcycleRentalUseCase : IRequestHandler<RequestMotorcycleRentalInput, Output>
    {
    }
}

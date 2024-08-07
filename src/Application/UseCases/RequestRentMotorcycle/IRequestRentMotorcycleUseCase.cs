using Application.Common;
using Application.UseCases.RequestRentMotorcycle.Inputs;
using MediatR;

namespace Application.UseCases.RequestRentMotorcycle
{
    public interface IRequestRentMotorcycleUseCase : IRequestHandler<RequestRentMotorcycleInput, Output>
    {
    }
}

using Application.Common;
using Application.UseCases.Motorcycle.DeleteMotorcycle.Inputs;
using MediatR;

namespace Application.UseCases.Motorcycle.DeleteMotorcycle
{
    public interface IDeleteMotorcycleUseCase : IRequestHandler<DeleteMotorcycleInput, Output>
    {
    }
}

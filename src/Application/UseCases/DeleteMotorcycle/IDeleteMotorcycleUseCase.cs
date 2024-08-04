using Application.Common;
using Application.UseCases.DeleteMotorcycle.Inputs;
using MediatR;

namespace Application.UseCases.DeleteMotorcycle
{
    public interface IDeleteMotorcycleUseCase : IRequestHandler<DeleteMotorcycleInput, Output>
    {
    }
}

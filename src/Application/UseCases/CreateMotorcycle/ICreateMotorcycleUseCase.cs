using Application.UseCases.CreateMotorcycle.Inputs;
using MediatR;

namespace Application.UseCases.CreateMotorcycle
{
    public interface ICreateMotorcycleUseCase : IRequestHandler<CreateMotorcycleInput, bool>
    {
    }
}

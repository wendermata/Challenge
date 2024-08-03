using Application.UseCases.CreateMotorcycle.Inputs;
using Application.UseCases.CreateMotorcycle.Outputs;
using MediatR;

namespace Application.UseCases.CreateMotorcycle
{
    public interface ICreateMotorcycleUseCase : IRequestHandler<CreateMotorcycleInput, CreateMotorcycleOutput>
    {
    }
}

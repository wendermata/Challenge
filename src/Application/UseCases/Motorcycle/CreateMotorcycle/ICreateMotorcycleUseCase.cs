using Application.UseCases.Motorcycle.CreateMotorcycle.Inputs;
using Application.UseCases.Motorcycle.CreateMotorcycle.Outputs;
using MediatR;

namespace Application.UseCases.Motorcycle.CreateMotorcycle
{
    public interface ICreateMotorcycleUseCase : IRequestHandler<CreateMotorcycleInput, CreateMotorcycleOutput>
    {
    }
}

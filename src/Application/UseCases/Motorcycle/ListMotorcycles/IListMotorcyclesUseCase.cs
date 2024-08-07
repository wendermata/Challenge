using Application.UseCases.Motorcycle.ListMotorcycles.Inputs;
using Application.UseCases.Motorcycle.ListMotorcycles.Outputs;
using MediatR;

namespace Application.UseCases.Motorcycle.ListMotorcycles
{
    public interface IListMotorcyclesUseCase : IRequestHandler<ListMotorcyclesInput, ListMotorcyclesOutput>
    {
    }
}

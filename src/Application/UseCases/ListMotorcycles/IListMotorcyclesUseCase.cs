using Application.UseCases.ListMotorcycles.Inputs;
using Application.UseCases.ListMotorcycles.Outputs;
using MediatR;

namespace Application.UseCases.ListMotorcycles
{
    public interface IListMotorcyclesUseCase : IRequestHandler<ListMotorcyclesInput, ListMotorcyclesOutput>
    {
    }
}

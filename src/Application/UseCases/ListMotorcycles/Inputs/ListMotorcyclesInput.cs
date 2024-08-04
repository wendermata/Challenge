using Application.UseCases.ListMotorcycles.Outputs;
using MediatR;

namespace Application.UseCases.ListMotorcycles.Inputs
{
    public class ListMotorcyclesInput : IRequest<ListMotorcyclesOutput>
    {
        public string Plate { get; set; }
    }
}

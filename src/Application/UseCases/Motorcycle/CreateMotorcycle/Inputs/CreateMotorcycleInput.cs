using Application.UseCases.Motorcycle.CreateMotorcycle.Outputs;
using MediatR;

namespace Application.UseCases.Motorcycle.CreateMotorcycle.Inputs
{
    public class CreateMotorcycleInput : IRequest<CreateMotorcycleOutput>
    {
        public int Year { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
    }
}

using Application.Common;
using MediatR;

namespace Application.UseCases.ModifyMotorcyclePlate.Inputs
{
    public class ModifyMotorcyclePlateInput : IRequest<Output>
    {
        public Guid Id { get; set; }
        public string NewPlate { get; set; }
    }
}

using Application.Common;
using MediatR;

namespace Application.UseCases.Motorcycle.DeleteMotorcycle.Inputs
{
    public class DeleteMotorcycleInput : IRequest<Output>
    {
        public Guid Id { get; set; }
    }
}

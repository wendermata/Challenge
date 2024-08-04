using Application.Common;
using MediatR;

namespace Application.UseCases.DeleteMotorcycle.Inputs
{
    public class DeleteMotorcycleInput : IRequest<Output>
    {
        public Guid Id { get; set; }
    }
}

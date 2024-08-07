using Application.Common;
using Application.UseCases.Renter.CreateRenter.Inputs;
using MediatR;

namespace Application.UseCases.Renter.CreateRenter
{
    public interface ICreateRenterUseCase : IRequestHandler<CreateRenterInput, Output>
    {
    }
}

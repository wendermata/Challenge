using Application.Common;
using Application.UseCases.CreateRenter.Inputs;
using MediatR;

namespace Application.UseCases.CreateRenter
{
    public interface ICreateRenterUseCase : IRequestHandler<CreateRenterInput, Output>
    {
    }
}

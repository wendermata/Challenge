using Application.Common;
using Application.UseCases.ModifyMotorcyclePlate.Inputs;
using MediatR;

namespace Application.UseCases.ModifyMotorcyclePlate
{
    public interface IModifyMotorcyclePlateUseCase : IRequestHandler<ModifyMotorcyclePlateInput, Output>
    {
    }
}

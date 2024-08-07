using Application.Common;
using Application.UseCases.Motorcycle.ModifyMotorcyclePlate.Inputs;
using MediatR;

namespace Application.UseCases.Motorcycle.ModifyMotorcyclePlate
{
    public interface IModifyMotorcyclePlateUseCase : IRequestHandler<ModifyMotorcyclePlateInput, Output>
    {
    }
}

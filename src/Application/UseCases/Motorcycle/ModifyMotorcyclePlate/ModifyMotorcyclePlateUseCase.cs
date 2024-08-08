using Application.Common;
using Application.UseCases.Motorcycle.ModifyMotorcyclePlate.Inputs;
using Domain.Repository;

namespace Application.UseCases.Motorcycle.ModifyMotorcyclePlate
{
    public class ModifyMotorcyclePlateUseCase : IModifyMotorcyclePlateUseCase
    {
        private readonly IMotorcycleRepository _repository;

        public ModifyMotorcyclePlateUseCase(IMotorcycleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Output> Handle(ModifyMotorcyclePlateInput request, CancellationToken cancellationToken)
        {
            var output = new Output();
            try
            {
                var motorcycle = await _repository.GetByIdAsync(request.Id, cancellationToken);
                if (motorcycle is null)
                {
                    output.ErrorMessages.Add($"Motorcycle with Id: {request.Id} not found!");
                    return output;
                }

                if (await _repository.CheckIfExistsAsync(request.NewPlate, cancellationToken))
                {
                    output.ErrorMessages.Add($"{request.NewPlate} already registered in database");
                    return output;
                }

                motorcycle.UpdatePlate(request.NewPlate);

                await _repository.UpdateAsync(motorcycle, cancellationToken);
                output.Messages.Add($"Motorcycle with Id: {request.Id} successfully update plate with: {request.NewPlate}");
                return output;
            }
            catch (Exception ex)
            {
                output.ErrorMessages.Add($"{ex.Message}");
                return output;
            }
        }
    }
}

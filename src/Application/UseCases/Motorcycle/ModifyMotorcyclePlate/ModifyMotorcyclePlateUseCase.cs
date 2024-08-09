using Application.Common;
using Application.Common.Helpers;
using Application.UseCases.Motorcycle.ModifyMotorcyclePlate.Inputs;
using Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Motorcycle.ModifyMotorcyclePlate
{
    public class ModifyMotorcyclePlateUseCase : IModifyMotorcyclePlateUseCase
    {
        private readonly IMotorcycleRepository _repository;
        private readonly ILogger<ModifyMotorcyclePlateUseCase> _logger;
        public ModifyMotorcyclePlateUseCase(IMotorcycleRepository repository, ILogger<ModifyMotorcyclePlateUseCase> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Output> Handle(ModifyMotorcyclePlateInput request, CancellationToken cancellationToken)
        {
            var output = new Output();
            try
            {
                var motorcycle = await _repository.GetByIdAsync(request.Id, cancellationToken);
                if (motorcycle is null)
                {
                    _logger.LogError($"Motorcycle with Id: {request.Id} not found!");
                    output.ErrorMessages.Add($"Motorcycle with Id: {request.Id} not found!");
                    return output;
                }

                if (await _repository.CheckIfExistsAsync(request.NewPlate, cancellationToken))
                {
                    _logger.LogError($"{request.NewPlate} already registered in database");
                    output.ErrorMessages.Add($"{request.NewPlate} already registered in database");
                    return output;
                }

                motorcycle.UpdatePlate(request.NewPlate);
                await _repository.UpdateAsync(motorcycle, cancellationToken);

                _logger.LogInformation($"Motorcycle with Id: {request.Id} successfully update plate with: {request.NewPlate}");
                output.Messages.Add($"Motorcycle with Id: {request.Id} successfully update plate with: {request.NewPlate}");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An error occurred while updating motorcycle plate: {ex.Message} request: {SerializeHelper.SerializeObjectToJson(request)}");
                output.ErrorMessages.Add($"{ex.Message}");
                return output;
            }
        }
    }
}

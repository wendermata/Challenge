using Application.Common;
using Application.UseCases.Motorcycle.DeleteMotorcycle.Inputs;
using Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Motorcycle.DeleteMotorcycle
{
    public class DeleteMotorcycleUseCase : IDeleteMotorcycleUseCase
    {
        private readonly IMotorcycleRepository _motorcycleRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly ILogger<DeleteMotorcycleUseCase> _logger;

        public DeleteMotorcycleUseCase(IMotorcycleRepository motorcycleRepository,
            IRentalRepository rentalRepository,
            ILogger<DeleteMotorcycleUseCase> logger)
        {
            _motorcycleRepository = motorcycleRepository;
            _rentalRepository = rentalRepository;
            _logger = logger;
        }

        public async Task<Output> Handle(DeleteMotorcycleInput request, CancellationToken cancellationToken)
        {
            var output = new Output();
            try
            {
                if (request == null)
                {
                    _logger.LogError($"Invalid request: {request}");
                    output.ErrorMessages.Add($"Invalid request: {request}");
                    return output;
                }

                var motorcycle = await _motorcycleRepository.GetByIdAsync(request.Id, cancellationToken);
                if (motorcycle is null)
                {
                    _logger.LogWarning($"Motorcycle with Id: {request.Id} not found!");
                    output.ErrorMessages.Add($"Motorcycle with Id: {request.Id} not found!");
                    return output;
                }

                var rentals = await _rentalRepository.GetRentalsByMotorcycleId(request.Id, cancellationToken);
                if (rentals.Count > 0)
                {
                    _logger.LogWarning($"Motorcycle with Id: {request.Id} can't be deleted because it was already used in past rentals!");
                    output.ErrorMessages.Add($"Motorcycle with Id: {request.Id} can't be deleted because it was already used in past rentals!");
                    return output;
                }

                motorcycle.Delete();
                await _motorcycleRepository.UpdateAsync(motorcycle, cancellationToken);
                _logger.LogInformation($"Motorcycle with Id: {request.Id} successfully deleted");
                output.Messages.Add($"Motorcycle with Id: {request.Id} successfully deleted");
                return output;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An error occurred while deleting motorcycle: {ex.Message}");
                output.ErrorMessages.Add($"{ex.Message}");
                return output;
            }
        }
    }
}

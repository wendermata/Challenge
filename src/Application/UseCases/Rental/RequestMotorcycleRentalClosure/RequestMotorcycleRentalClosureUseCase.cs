using Application.UseCases.Rental.RequestMotorcycleRentalClosure.Inputs;
using Application.UseCases.Rental.RequestMotorcycleRentalClosure.Mapping;
using Application.UseCases.Rental.RequestMotorcycleRentalClosure.Outputs;
using Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rental.RequestMotorcycleRentalClosure
{
    public class RequestMotorcycleRentalClosureUseCase : IRequestMotorcycleRentalClosureUseCase
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly ILogger<RequestMotorcycleRentalClosureUseCase> _logger;

        public RequestMotorcycleRentalClosureUseCase(IRentalRepository rentalRepository, ILogger<RequestMotorcycleRentalClosureUseCase> logger)
        {
            _rentalRepository = rentalRepository;
            _logger = logger;
        }

        public async Task<RequestMotorcycleRentalClosureOutput> Handle(RequestMotorcycleRentalClosureInput request, CancellationToken cancellationToken)
        {
            var output = new RequestMotorcycleRentalClosureOutput();
            try
            {
                var rental = await _rentalRepository.GetByIdAsync(request.RentalId, cancellationToken);
                if (rental == null)
                {
                    _logger.LogWarning($"Rental with id {request.RentalId} not found");
                    output.ErrorMessages.Add($"Rental with id {request.RentalId} not found");
                    return output;
                }

                if (rental.IsFinished)
                {
                    _logger.LogWarning($"Rental with id {request.RentalId} was already closed in {rental.DevolutionDate!.Value:YYYY-mm-dd}");
                    output.ErrorMessages.Add($"Rental with id {request.RentalId} was already closed in {rental.DevolutionDate!.Value:YYYY-mm-dd}");
                    return output;
                }

                rental.FinishRental(request.ClosureDate);
                await _rentalRepository.UpdateAsync(rental, cancellationToken);

                _logger.LogInformation($"Rental with id {request.RentalId} successfully closed in {request.ClosureDate:YYYY-mm-dd}");
                return rental.MapToOutput(); 
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An error occurred while closing rental with id {request.RentalId}: {ex.Message}");
                output.ErrorMessages.Add($"{ex.Message}");
                return output;
            }
        }
    }
}

using Application.Common;
using Application.UseCases.Rental.RequestMotorcycleRental.Inputs;
using Application.UseCases.Rental.RequestMotorcycleRental.Mapping;
using Application.UseCases.Rental.RequestRentMotorcycle;
using Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Rental.RequestMotorcycleRental
{
    public class RequestMotorcycleRentalUseCase : IRequestMotorcycleRentalUseCase
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IRenterRepository _renterRepository;
        private readonly ILogger<RequestMotorcycleRentalUseCase> _logger;

        public RequestMotorcycleRentalUseCase(IRentalRepository rentalRepository, IRenterRepository renterRepository, ILogger<RequestMotorcycleRentalUseCase> logger)
        {
            _rentalRepository = rentalRepository;
            _renterRepository = renterRepository;
            _logger = logger;
        }

        public async Task<Output> Handle(RequestMotorcycleRentalInput request, CancellationToken cancellationToken)
        {
            Output output = new();
            try
            {
                var renter = await _renterRepository.GetByIdAsync(request.RenterId, cancellationToken);
                if (renter.CanRent() is false)
                {
                    _logger.LogError($"Renter {request.RenterId} can't rent motorcycle.");
                    output.ErrorMessages.Add($"Renter {request.RenterId} can't rent motorcycle.");
                    return output;
                }
                var rental = request.MapToDomain();
                await _rentalRepository.InsertAsync(rental, cancellationToken);
                
                _logger.LogInformation($"Rental {rental.Id} created.");
                output.Messages.Add($"Rental {rental.Id} created.");

                return output;
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"An error occurred while creating rental: {ex.Message}");
                output.ErrorMessages.Add($"{ex.Message}");
                return output;
            }
        }
    }
}

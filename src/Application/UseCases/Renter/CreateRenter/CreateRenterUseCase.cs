using Application.Common;
using Application.UseCases.Renter.CreateRenter.Inputs;
using Application.UseCases.Renter.CreateRenter.Mapping;
using Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Renter.CreateRenter
{
    public class CreateRenterUseCase : ICreateRenterUseCase
    {
        private readonly IRenterRepository _renterRepository;
        private readonly ILogger<CreateRenterUseCase> _logger;

        public CreateRenterUseCase(IRenterRepository renterRepository, ILogger<CreateRenterUseCase> logger)
        {
            _renterRepository = renterRepository;
            _logger = logger;
        }

        public async Task<Output> Handle(CreateRenterInput request, CancellationToken cancellationToken)
        {
            Output output = new();
            try
            {
                if (await _renterRepository.GetByDocumentAsync(request.Document, cancellationToken) != null)
                {
                    _logger.LogInformation($"Document '{request.Document}' already registered in database");
                    output.ErrorMessages.Add($"Document '{request.Document}' already registered in database");
                    return output;
                }

                if (await _renterRepository.GetByLicenseAsync(request.LicenseNumber, cancellationToken) != null)
                {
                    _logger.LogInformation($"License number '{request.LicenseNumber}' already registered in database");
                    output.ErrorMessages.Add($"License number '{request.LicenseNumber}' already registered in database");
                    return output;
                }

                var renter = request.MapToDomain();
                await _renterRepository.InsertAsync(renter, cancellationToken);
                _logger.LogInformation($"Renter {renter.Id} created.");

                return output;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"An error occurred while creating renter: {ex.Message}");
                output.ErrorMessages.Add($"{ex.Message}");
                return output;
            }
        }
    }
}

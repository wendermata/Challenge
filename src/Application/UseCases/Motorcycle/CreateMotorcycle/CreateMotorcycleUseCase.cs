using Application.Common.Helpers;
using Application.UseCases.Motorcycle.CreateMotorcycle.Inputs;
using Application.UseCases.Motorcycle.CreateMotorcycle.Mapping;
using Application.UseCases.Motorcycle.CreateMotorcycle.Outputs;
using Domain.Repository;
using Infra.Kafka.Producer;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Motorcycle.CreateMotorcycle
{
    public class CreateMotorcycleUseCase : ICreateMotorcycleUseCase
    {
        private readonly IMotorcycleRepository _repository;
        private readonly IProducerService _producerService;
        private readonly ILogger<CreateMotorcycleUseCase> _logger;

        public CreateMotorcycleUseCase(IMotorcycleRepository motorcycleRepository, IProducerService producerService, ILogger<CreateMotorcycleUseCase> logger)
        {
            _repository = motorcycleRepository;
            _producerService = producerService;
            _logger = logger;
        }

        public async Task<CreateMotorcycleOutput> Handle(CreateMotorcycleInput request, CancellationToken cancellationToken)
        {
            var output = new CreateMotorcycleOutput();
            try
            {
                var domain = request.MapToDomain();
                if (domain == null)
                {
                    _logger.LogError($"Invalid request: {request}");
                    output.ErrorMessages.Add($"Invalid request: {request}");
                    return output;
                }

                if (await _repository.CheckIfExistsAsync(domain.Plate, cancellationToken))
                {
                    _logger.LogError($"{domain.Plate} already registered in database");
                    output.ErrorMessages.Add($"{domain.Plate} already registered in database");
                    return output;
                }

                await _repository.InsertAsync(domain, cancellationToken);

                if (domain.Year == 2024)
                {
                    var message = SerializeHelper.SerializeObjectToJson(request);
                    _logger.LogInformation($"Message successfully sent: {message}");
                    await _producerService.ProduceAsync(message);
                }

                output = domain.MapToOutput();
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

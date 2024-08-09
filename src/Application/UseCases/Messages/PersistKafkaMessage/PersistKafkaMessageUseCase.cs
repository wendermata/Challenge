using Application.Common;
using Application.Common.Helpers;
using Application.UseCases.Messages.PersistKafkaMessage.Inputs;
using Application.UseCases.Messages.PersistKafkaMessage.Mappings;
using Domain.Repository;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.Messages.PersistKafkaMessage
{
    public class PersistKafkaMessageUseCase : IPersistKafkaMessageUseCase
    {
        private readonly IKafkaMessageRepository _repository;
        private readonly ILogger<PersistKafkaMessageUseCase> _logger;
        public PersistKafkaMessageUseCase(IKafkaMessageRepository repository, ILogger<PersistKafkaMessageUseCase> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Output> Handle(PersistKafkaMessageInput request, CancellationToken cancellationToken)
        {
            var output = new Output();
            try
            {
                if (request.Value is null)
                {
                    _logger.LogError($"Invalid request: {request}");
                    output.ErrorMessages.Add($"Invalid request: {request}");
                    return output;
                }
                var message = request.MapToDomain();

                await _repository.InsertAsync(request.MapToDomain(), cancellationToken);
                _logger.LogError($"Successfully inserted: {SerializeHelper.SerializeObjectToJson(request)}");

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

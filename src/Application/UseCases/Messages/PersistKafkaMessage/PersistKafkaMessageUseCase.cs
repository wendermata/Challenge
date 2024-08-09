using Application.Common;
using Application.UseCases.Messages.PersistKafkaMessage.Inputs;
using Application.UseCases.Messages.PersistKafkaMessage.Mappings;
using Domain.Repository;

namespace Application.UseCases.Messages.PersistKafkaMessage
{
    public class PersistKafkaMessageUseCase : IPersistKafkaMessageUseCase
    {
        private readonly IKafkaMessageRepository _repository;

        public async Task<Output> Handle(PersistKafkaMessageInput request, CancellationToken cancellationToken)
        {
            var output = new Output();
            try
            {
                if (request.Value is null)
                {
                    output.ErrorMessages.Add($"Invalid request: {request}");
                    return output;
                }
                var message = request.MapToDomain();
                await _repository.InsertAsync(request.MapToDomain(), cancellationToken);
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

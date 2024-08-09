using Application.Common;
using Application.UseCases.Messages.PersistKafkaMessage.Inputs;
using MediatR;

namespace Application.UseCases.Messages.PersistKafkaMessage
{
    public interface IPersistKafkaMessageUseCase : IRequestHandler<PersistKafkaMessageInput, Output>
    {
    }
}

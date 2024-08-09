using Application.Common;
using MediatR;

namespace Application.UseCases.Messages.PersistKafkaMessage.Inputs
{
    public class PersistKafkaMessageInput : IRequest<Output>
    {
        public string Value { get; set; }
    }
}

using Application.UseCases.Messages.PersistKafkaMessage.Inputs;
using Domain.Entities;

namespace Application.UseCases.Messages.PersistKafkaMessage.Mappings
{
    public static class PersistKafkaMessageInputMapper
    {
        public static PersistKafkaMessageInput MapToInput(this string value)
        {
            if (value is null)
                return null;

            return new PersistKafkaMessageInput
            {
                Value = value
            };
        }

        public static KafkaMessage MapToDomain(this PersistKafkaMessageInput input)
        {
            if (input is null)
                return null;

            return new KafkaMessage(input.Value);
        }
    }
}

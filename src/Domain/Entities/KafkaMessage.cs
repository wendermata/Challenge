using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Domain.Entities
{
    public class KafkaMessage
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; private set; }
        public string Request { get; private set; }
        public DateTime ReceivedAt { get; private set; }

        public KafkaMessage() { }
        public KafkaMessage(string value)
        {
            Id = Guid.NewGuid();
            Request = value;
            ReceivedAt = DateTime.Now;
        }
    }
}

using Domain.Entities;

namespace Domain.Repository
{
    public interface IKafkaMessageRepository : IRepository<KafkaMessage>
    {
    }
}

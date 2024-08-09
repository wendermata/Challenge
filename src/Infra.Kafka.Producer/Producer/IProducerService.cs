namespace Infra.Kafka.Producer
{
    public interface IProducerService
    {
        Task ProduceAsync(string message);
    }
}

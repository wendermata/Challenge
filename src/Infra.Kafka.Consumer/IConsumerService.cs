namespace Infra.Kafka.Consumer
{
    public interface IConsumerService
    {
        Task ConsumeAsync();
    }
}

using Confluent.Kafka;
using Infra.Kafka.Settings;
using Microsoft.Extensions.Logging;

namespace Infra.Kafka.Producer
{
    public class ProducerService : IProducerService
    {
        private readonly KafkaSettings _settings;
        private readonly ProducerConfig _config;
        private readonly IProducer<Null, string> _producer;

        private readonly ILogger<ProducerService> _logger;

        public ProducerService(KafkaSettings settings, ILogger<ProducerService> logger)
        {
            _settings = settings;
            _config = new ProducerConfig
            {
                BootstrapServers = _settings.BootstrapServers
            };

            _producer = new ProducerBuilder<Null, string>(_config).Build();
            _logger = logger;
        }

        public async Task ProduceAsync(string message)
        {
            _producer.Produce(_settings.Topic, new Message<Null, string> { Value = message }, (deliveryReport) =>
            {
                if (deliveryReport.Error != null && deliveryReport.Error.Reason != "Success")
                {
                    _logger.LogError($"Delivery Error: {deliveryReport.Error.Reason}");
                    return;
                }
                _logger.LogInformation($"Delivered '{deliveryReport.Value}' to '{deliveryReport.TopicPartitionOffset}'");
            });
        }
    }
}

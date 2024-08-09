using Application.UseCases.Messages.PersistKafkaMessage.Mappings;
using Confluent.Kafka;
using Infra.Kafka.Settings;
using MediatR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infra.Kafka.Consumer
{
    public class ConsumerService : IConsumerService, IHostedService
    {
        private readonly KafkaSettings _settings;
        private readonly ConsumerConfig _config;
        private readonly IConsumer<Null, string> _consumer;
        private readonly IMediator _mediator;

        private readonly ILogger<ConsumerService> _logger;
        public ConsumerService(KafkaSettings settings, IMediator mediator, ILogger<ConsumerService> logger)
        {
            _settings = settings;
            _mediator = mediator;
            _config = new ConsumerConfig
            {
                GroupId = _settings.GroupId,
                BootstrapServers = _settings.BootstrapServers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            _consumer = new ConsumerBuilder<Null, string>(_config).Build();
        }

        public async Task ConsumeAsync()
        {
            _consumer.Subscribe(_settings.Topic);
            await Task.Run(() =>
            {
                while (true)
                {
                    try
                    {
                        var cr = _consumer.Consume();
                        _logger.LogInformation($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
                        var input = cr.Value.MapToInput();
                        _mediator.Send(input);
                    }
                    catch (ConsumeException e)
                    {
                        _logger.LogError($"Error occurred: {e.Error.Reason}");
                    }
                }
            });
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => ConsumeAsync());
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
           _consumer.Close();
        }

    }
}

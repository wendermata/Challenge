using Microsoft.Extensions.DependencyInjection;

namespace Infra.Kafka.Consumer.Extensions
{
    public static class KafkaConsumerExtensions
    {
        public static IServiceCollection AddKakfaConsumers(this IServiceCollection services)
        {
            services.AddSingleton<IConsumerService, ConsumerService>();
            services.AddHostedService<ConsumerService>();
            return services;
        }
    }
}

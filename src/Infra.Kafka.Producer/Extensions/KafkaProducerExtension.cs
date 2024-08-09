using Infra.Kafka.Producer;
using Infra.Kafka.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infra.Kafka.Producer.Extensions
{
    public static class KafkaProducerExtension
    {
        public static IServiceCollection AddKakfaProducers(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<KafkaSettings>(configuration.GetSection(nameof(KafkaSettings)));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<KafkaSettings>>().Value);
            services.AddSingleton<IProducerService, ProducerService>();
            return services;
        }
    }
}

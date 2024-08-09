using Domain.Entities;
using Domain.Repository;
using Infra.Mongo.Repositories;
using Infra.Mongo.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infra.Mongo.Extensions
{
    public static class MongoExtension
    {
        public static IServiceCollection AddMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ChallengeDatabaseSettings>(configuration.GetSection(nameof(ChallengeDatabaseSettings)));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<ChallengeDatabaseSettings>>().Value);
            services.AddSingleton<IMongoService, MongoService>();
            services.AddRepositories();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IKafkaMessageRepository, KafkaMessageRepository>();
            services.AddTransient<IMotorcycleRepository, MotorcycleRepository>();
            services.AddTransient<IRentalRepository, RentalRepository>();
            services.AddTransient<IRenterRepository, RenterRepository>();

            return services;
        }

        private static void RegisterMongoMappings()
        {
            BsonClassMap.RegisterClassMap<KafkaMessage>(map =>
            {
                map.AutoMap();
                map.MapProperty(x => x.Id)
                    .SetSerializer(new GuidSerializer(BsonType.String));
            });

            BsonClassMap.RegisterClassMap<Motorcycle>(map =>
            {
                map.AutoMap();
                map.MapProperty(x => x.Id)
                    .SetSerializer(new GuidSerializer(BsonType.String));
            });

            BsonClassMap.RegisterClassMap<Renter>(map =>
            {
                map.AutoMap();
                map.MapProperty(x => x.Id)
                    .SetSerializer(new GuidSerializer(BsonType.String));
            });

            BsonClassMap.RegisterClassMap<Rental>(map =>
            {
                map.AutoMap();
                map.MapProperty(x => x.Id)
                    .SetSerializer(new GuidSerializer(BsonType.String));
                map.MapProperty(x => x.RenterId)
                    .SetSerializer(new GuidSerializer(BsonType.String));
                map.MapProperty(x => x.MotorcycleId)
                    .SetSerializer(new GuidSerializer(BsonType.String));

            });
        }
    }
}

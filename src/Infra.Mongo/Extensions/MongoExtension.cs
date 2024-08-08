using Domain.Repository;
using Infra.Mongo.Repositories;
using Infra.Mongo.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
            services.AddTransient<IMotorcycleRepository, MotorcycleRepository>();
            services.AddTransient<IRentalRepository, RentalRepository>();
            services.AddTransient<IRenterRepository, RenterRepository>();

            return services;
        }
    }
}

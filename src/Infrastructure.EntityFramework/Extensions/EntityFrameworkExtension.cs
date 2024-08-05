using Domain.Repository;
using Infrastructure.EntityFramework.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EntityFramework.Extensions
{
    public static class EntityFrameworkExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IMotorcycleRepository, MotorcycleRepository>();
            services.AddTransient<IRentalRepository, RentalRepository>();
            services.AddTransient<IRenterRepository, RenterRepository>();
            return services;
        }
    }
}

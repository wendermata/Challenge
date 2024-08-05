using Application.Interfaces;
using Domain.Repository;
using Infrastructure.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EntityFramework.Extensions
{
    public static class EntityFrameworkExtension
    {
        public static IServiceCollection AddConections(this IServiceCollection services)
        {
            services.AddDbConnection();
            return services;
        }

        private static IServiceCollection AddDbConnection(this IServiceCollection services)
        {
            services.AddDbContext<ChallengeDbContext>(opt => opt.UseInMemoryDatabase("teste"));
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IMotorcycleRepository, MotorcycleRepository>();
            services.AddTransient<IRentalRepository, RentalRepository>();
            services.AddTransient<IRenterRepository, RenterRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
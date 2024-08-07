using Application.UseCases.Motorcycle.CreateMotorcycle;
using Application.UseCases.Motorcycle.DeleteMotorcycle;
using Application.UseCases.Motorcycle.ListMotorcycles;
using Application.UseCases.Motorcycle.ModifyMotorcyclePlate;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ApplicationExtension).Assembly));

            services.AddTransient<ICreateMotorcycleUseCase, CreateMotorcycleUseCase>();
            services.AddTransient<IDeleteMotorcycleUseCase, DeleteMotorcycleUseCase>();
            services.AddTransient<IModifyMotorcyclePlateUseCase, ModifyMotorcyclePlateUseCase>();
            services.AddTransient<IListMotorcyclesUseCase, ListMotorcyclesUseCase>();
            return services;
        }
    }
}
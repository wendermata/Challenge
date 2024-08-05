using Application.UseCases.CreateMotorcycle;
using Application.UseCases.DeleteMotorcycle;
using Application.UseCases.ListMotorcycles;
using Application.UseCases.ModifyMotorcyclePlate;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<ICreateMotorcycleUseCase, CreateMotorcycleUseCase>();
            services.AddTransient<IDeleteMotorcycleUseCase, DeleteMotorcycleUseCase>();
            services.AddTransient<IModifyMotorcyclePlateUseCase, ModifyMotorcyclePlateUseCase>();
            services.AddTransient<IListMotorcyclesUseCase, ListMotorcyclesUseCase>();
            return services;
        }
    }
}

using Application.Boundaries.Services.S3;
using Application.Boundaries.Services.S3.Settings;
using Application.UseCases.Motorcycle.CreateMotorcycle;
using Application.UseCases.Motorcycle.DeleteMotorcycle;
using Application.UseCases.Motorcycle.ListMotorcycles;
using Application.UseCases.Motorcycle.ModifyMotorcyclePlate;
using Application.UseCases.Rental.RequestMotorcycleRental;
using Application.UseCases.Rental.RequestMotorcycleRentalClosure;
using Application.UseCases.Rental.RequestRentMotorcycle;
using Application.UseCases.Renter.CreateRenter;
using Application.UseCases.Renter.UploadRenterLicenseImage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
            services.AddTransient<IRequestMotorcycleRentalUseCase, RequestMotorcycleRentalUseCase>();
            services.AddTransient<IRequestMotorcycleRentalClosureUseCase, RequestMotorcycleRentalClosureUseCase>();
            services.AddTransient<ICreateRenterUseCase, CreateRenterUseCase>();
            services.AddTransient<IUploadRenterLicenseImageUseCase, UploadRenterLicenseImageUseCase>();

            return services;
        }

        public static IServiceCollection AddBoundaries(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddS3Service(configuration);
            return services;
        }

        private static IServiceCollection AddS3Service(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<S3Settings>(configuration.GetSection(nameof(S3Settings)));
            services.AddSingleton(sp => sp.GetRequiredService<IOptions<S3Settings>>().Value);
            services.AddTransient<IS3Service, S3Service>();
            return services;
        }
    }
}
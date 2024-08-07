using System.Diagnostics.CodeAnalysis;

namespace WebApi.Extensions
{
    public static class ControllerExtensions
    {
        [ExcludeFromCodeCoverage]
        public static IServiceCollection AddConfigureControllers(this IServiceCollection services)
        {
            services.AddControllers();
            services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });
            return services;
        }
    }
}

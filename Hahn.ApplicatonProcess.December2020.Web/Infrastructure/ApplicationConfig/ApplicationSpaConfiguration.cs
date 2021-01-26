using Microsoft.Extensions.DependencyInjection;

namespace Hahn.ApplicationProcess.December2020.Web.Infrastructure.ApplicationConfig
{
    public static class ApplicationSpaConfiguration
    {
        public static void AddApplicationSpa(this IServiceCollection services)
        {
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }
    }
}
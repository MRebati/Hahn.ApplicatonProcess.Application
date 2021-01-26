using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace Hahn.ApplicationProcess.December2020.Web.Infrastructure.ApplicationConfig
{
    public static class ApplicationLoggerConfiguration
    {
        public static void UseRequestLogging(this IApplicationBuilder app)
        {
            app.UseSerilogRequestLogging();

            //Log.Logger = new LoggerConfiguration()
            //    .WriteTo.File("/wwwroot/logs/", LogEventLevel.Debug)
            //    .WriteTo.Console(Serilog.Events.LogEventLevel.Debug)
            //    .MinimumLevel.Debug()
            //    .Enrich.FromLogContext()
            //    .CreateLogger();
        }
    }
}
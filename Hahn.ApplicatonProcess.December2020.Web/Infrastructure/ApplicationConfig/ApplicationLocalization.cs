using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Hahn.ApplicationProcess.December2020.Web.Localization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi.Models;

namespace Hahn.ApplicationProcess.December2020.Web.Infrastructure.ApplicationConfig
{
    public static class ApplicationLocalization
    {
        public static void AddApplicationLocalization(this IServiceCollection services)
        {
            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            services.AddSingleton<IStringLocalizer, JsonStringLocalizer>();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
        }

    }
}

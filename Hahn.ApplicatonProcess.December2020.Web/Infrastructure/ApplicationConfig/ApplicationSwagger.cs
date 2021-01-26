using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Hahn.ApplicationProcess.December2020.Web.Infrastructure.ApplicationConfig
{
    public static class ApplicationSwagger
    {
        public static void AddApplicationSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = "Applicant Service Api",
                    Description = "A simple test application service for evaluation",
                    TermsOfService = new Uri("https://hahn.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "MRebati",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/mrebati"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under MIT",
                        Url = new Uri("https://hahn.com/license"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public static void UseApplicationSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("V1/swagger.json", "Applicant Service Api");
            });
        }
    }
}

using System;
using System.Reflection;
using System.IO;
using Aurelia.AspNetCore.SpaServices.AureliaCli.SpaServices.AureliaCli;
using FluentValidation.AspNetCore;
using Hahn.ApplicationProcess.December2020.Web.Infrastructure.ApplicationConfig;
using Hahn.ApplicationProcess.December2020.Web.Infrastructure.DependencyResolver;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;

namespace Hahn.ApplicationProcess.December2020.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Services are added here into the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDatabase(Configuration, true);
            services.AddControllersWithViews().AddFluentValidation(s =>
            {
                s.RegisterValidatorsFromAssemblyContaining<Startup>();
                s.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            }); 

            services.AddModelValidators();
            services.AddApplicationLocalization();
            services.AddApplicationSwagger();
            services.AddApplicationSpa();
            services.AddApplicationRepositories();
            services.AddApplicationUnitOfWorks();
            services.AddApplicationServices();

            //removed from here and added the configuration to appsettings.json
            //services.AddApplicationLogger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseApplicationSwagger();
            app.UseRequestLogging();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //spa.UseAureliaCliServer("start");

                    //we can proxy to the application on a manual "npm start" command running in clienApp folder
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:8080");
                }
            });
        }
    }
}

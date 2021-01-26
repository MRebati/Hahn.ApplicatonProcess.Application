using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Hahn.ApplicationProcess.December2020.Data.Infrastructure;
using Hahn.ApplicationProcess.December2020.Data.Repositories;
using Hahn.ApplicationProcess.December2020.Data.UnitsOfWork;
using Hahn.ApplicationProcess.December2020.Domain.Entities;
using Hahn.ApplicationProcess.December2020.Domain.Repositories;
using Hahn.ApplicationProcess.December2020.Domain.Services.ApplicantService;
using Hahn.ApplicationProcess.December2020.Domain.Services.ApplicantService.Dto;
using Hahn.ApplicationProcess.December2020.Domain.Services.CommonService;
using Hahn.ApplicationProcess.December2020.Domain.UnitsOfWork;
using Hahn.ApplicationProcess.December2020.Domain.Utilities;
using Hahn.ApplicationProcess.December2020.Domain.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hahn.ApplicationProcess.December2020.Web.Infrastructure.DependencyResolver
{
    public static class Resolver
    {
        public static void AddApplicationRepositories(this IServiceCollection services)
        {
            services.AddScoped<IApplicantRepository, ApplicantRepository>();
        }

        public static void AddApplicationUnitOfWorks(this IServiceCollection services)
        {
            services.AddScoped<IManagementUnitOfWork, ManagementUnitOfWork>();
        }

        public static void AddModelValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<ApplicantInputDto>, ApplicantValidator>();
        }

        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<ICacheManagement, CacheManagement>();
            services.AddScoped<IApplicantService, ApplicantService>();
            services.AddScoped<ICommonService, CommonService>();
        }

        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration, bool inMemoryDatabase = false)
        {
            if (inMemoryDatabase)
                services.AddDbContext<ApplicationDbContext>(op => op.UseInMemoryDatabase(databaseName: "ApplicantDatabase"));
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly("Hahn.ApplicationProcess.December2020.Data")), ServiceLifetime.Singleton);
            }

            services.AddScoped<DbContext, ApplicationDbContext>();
        }
    }
}

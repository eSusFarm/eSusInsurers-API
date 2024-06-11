using eSusInsurers.Domain;
using eSusInsurers.Domain.Entities;
using eSusInsurers.Infrastructure.Common;
using eSusInsurers.Infrastructure.Interfaces;
using eSusInsurers.Infrastructure.Persistence;
using eSusInsurers.Infrastructure.Repositories;
using eSusInsurers.Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace eSusInsurers.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddSingleton<ISaveChangesInterceptor, AuditableEntityInterceptor>();

            if (!env.IsProduction())
            {
                services.AddDbContextPool<eSusInsurerContext>((sp, options) =>
                {
                    options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

                    options.UseSqlServer(configuration.GetConnectionString("DbConnection"))
                           .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()))
                           .EnableSensitiveDataLogging();
                });
              
            }
            else
            {
                services.AddDbContextPool<eSusInsurerContext>((sp, options) =>
                {
                    options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

                    options.UseSqlServer(configuration.GetConnectionString("DbConnection"))
                           .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddDebug()))
                           .EnableSensitiveDataLogging();
                });
            }

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IDateTime, DateTimeService>();

            services.AddMemoryCache();

            return services;
        }
    }
}

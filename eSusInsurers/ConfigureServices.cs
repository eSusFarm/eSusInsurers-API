using EmailService.Interfaces;
using EmailService.Services;
using eSusInsurers.Common.Logging;
using eSusInsurers.ConfigServices;
using eSusInsurers.Helpers;
using eSusInsurers.Services;
using eSusInsurers.Services.Implementations;
using eSusInsurers.Services.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;
using ILogger = Serilog.ILogger;

namespace eSusInsurers
{
    /// <summary>
    /// Configures the services for WebApi
    /// </summary>
    public static class ConfigureServices
    {
        /// <summary>
        /// Add the services in the Service Collection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddWebApiServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddControllerServices(configuration)
                .AddRouting()
                .AddEndpointsApiExplorer()
                .AddSwaggerConfiguration();

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.SaveToken = true;
               options.RequireHttpsMetadata = false;
               options.TokenValidationParameters = new TokenValidationParameters()
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ClockSkew = TimeSpan.Zero,

                   ValidAudience = configuration["JWT:ValidAudience"],
                   ValidIssuer = configuration["JWT:ValidIssuer"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
               };
           });

            services.AddSwaggerExamplesFromAssemblies(typeof(Program).Assembly);
            return services;
        }

        /// <summary>
        /// Configures the services for ApplicationServices
        /// </summary>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddLogging(builder =>
            {
                ILogger logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();

                services.AddSingleton(logger);
            });
            services.AddHttpClient<IEsusFarmPolicyService, EsusFarmPolicyService>(client =>
            {
                client.BaseAddress = new Uri("https://api.esusfarm.etherisc.com/");
            });

            services.AddScoped(typeof(ILoggerContext<>), typeof(LoggerContext<>));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            //Register services
            services.AddTransient(typeof(IInsuranceProviderService), typeof(InsuranceProviderService));
            services.AddTransient(typeof(IUserService), typeof(UserService));
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IUpdateNotificationTemplate, UpdateNotificationTemplate>();
            services.AddTransient<IEmailService, Services.Implementations.EmailService>();
            services.AddTransient<IRolesService, RolesService>();
            services.AddTransient<ISeasonService, SeasonService>();
            services.AddTransient<ISeasonCutOffDateService, SeasonCutOffDateService>();
            services.AddTransient<IInsuranceProductService, InsuranceProductService>();
            services.AddTransient<FireForget>();
            services.AddTransient<ICountriesService, CountriesService>();
            services.AddTransient<IProgramsService, ProgramService>();
            services.AddTransient<ICropCategoryService, CropCategoryService>();
            services.AddTransient<ICropService, CropService>();
            services.AddTransient<IInsuranceCompanyService, InsuranceCompanyService>();
            services.AddTransient<IEsusFarmPolicyService, EsusFarmPolicyService>();

            return services;
        }
    }
}

using eSusInsurers.Filter;
using eSusInsurers.Swagger.OperationFilters;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace eSusInsurers.ConfigServices
{
    /// <summary>
    /// Helper class for Swagger configuration.
    /// </summary>
    public static class SwaggerHelper
    {
        /// <summary>
        /// Adds Swagger configuration to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add Swagger configuration to.</param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "eSusInsurer", Version = "v1" });

                // Include XML comments in the Swagger output (optional)
                var xmlFile = $"eSusInsurers.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swagger.IncludeXmlComments(xmlPath);

                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "JWT token must be provided",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                swagger.OperationFilter<ParametersOperationFilter>();
                swagger.OperationFilter<AuthorizeCheckOperationFilter>();

            });

            services.AddFluentValidationRulesToSwagger();

            return services;
        }
    }
}

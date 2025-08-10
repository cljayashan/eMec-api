using System.Text;
using emec.business.managers;
using emec.contracts.managers;
using emec.contracts.repositories;
using emec.data.repositories;
using emec.dbcontext.tables.Models;
using emec.entities.HealthCheck;
using emec.shared.Contracts;
using emec.shared.Mappers;
using emec.shared.models;
using Microsoft.IdentityModel.Tokens;
using emec.business.validators.HealthCheck;
using emec.shared.Errors;
using Microsoft.EntityFrameworkCore;

namespace emec.api
{
    public static class ServiceExtentions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register your custom services here  
            // Example: services.AddScoped<IMyService, MyService>();

            // JWT Authentication configuration
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),

                    ClockSkew = TimeSpan.Zero //normally jqwt tokens have a 5 minute clock skww but we set it to zero
                };
            });


            //DbContext configuration
            services.AddDbContext<EMecContext>(options => options.UseSqlServer(configuration["AppConfiguration:ConnectionString"]));

            //Configurations
            //services.Configure<AppConfiguration>(configuration.GetSection("AppConfiguration"));

            //Managers
            services.AddScoped<IHealthCheckManager, HealthCheckManager>();

            //Validators
            services.AddScoped<IValidator<HealthCheckDataRequest>, HealthCheckRequestValidator>();

            //Mappers
            services.AddSingleton<IMapper<ResponseMessage, ResponseBase>, ServiceErrorMapper>();
            services.AddSingleton<IMapper<object, ResponseBase>, ServiceResponseMapper>();

            //Repositories
            services.AddScoped<IHealthCheckRepository, HealthCheckRepository>();

            //resource
            services.AddScoped<IErrorMessages, ErrorMessages>();

            //Handlers


            return services;
        }
    }
}

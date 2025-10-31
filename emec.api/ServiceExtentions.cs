using System.Text;
using emec.business.managers;
using emec.business.mappers;
using emec.business.validators.Customer;
using emec.business.validators.HealthCheck;
using emec.business.validators.Login;
using emec.business.validators.Vehicle;
using emec.contracts.managers;
using emec.contracts.repositories;
using emec.data.mappers;
using emec.data.repositories;
using emec.dbcontext.tables.Models;
using emec.entities.Customer;
using emec.entities.Customer.View;
using emec.entities.Customer.Delete;
using emec.entities.HealthCheck;
using emec.entities.Login;
using emec.entities.Vehicle.Register;
using emec.entities.Vehicle.List;
using emec.shared.Contracts;
using emec.shared.Errors;
using emec.shared.Mappers;
using emec.shared.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            services.AddDbContext<eMecContext>(options => options.UseSqlServer(configuration["AppConfiguration:ConnectionString"]));

            //Configurations
            //services.Configure<AppConfiguration>(configuration.GetSection("AppConfiguration"));

            //Managers
            services.AddScoped<IHealthCheckManager, HealthCheckManager>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<IVehicleManager, VehicleManager>();
            services.AddScoped<ICustomerManager, CustomerManager>();

            //Validators
            services.AddScoped<IValidator<HealthCheckDataRequest>, HealthCheckRequestValidator>();
            services.AddScoped<IValidator<LoginDataRequest>, LoginRequestValidator>();
            services.AddScoped<IValidator<VehicleRegisterDataRequest>, VehicleRegisterRequestValidator>();
            services.AddScoped<IValidator<VehicleListDataRequest>, VehicleListDataRequestValidator>();
            services.AddScoped<IValidator<CustomerDataRequest>, SearchCustomerDataRequestValidator>();
            services.AddScoped<IValidator<CustomerViewRequest>, CustomerViewRequestValidator>();
            services.AddScoped<IValidator<CustomerDeleteRequest>, CustomerDeleteRequestValidator>();

            //Mappers
            services.AddSingleton<IMapper<ResponseMessage, ResponseBase>, ServiceErrorMapper>();
            services.AddSingleton<IMapper<object, ResponseBase>, ServiceResponseMapper>();
            services.AddScoped<IMapper<VehicleRegisterDataRequest, VehicleRegistrationDataSave>, VehicleRegistrationDataSaveRequestMapper>();

            //Repositories
            services.AddScoped<IHealthCheckRepository, HealthCheckRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            //resource
            services.AddScoped<IErrorMessages, ErrorMessages>();
            services.AddScoped<IEntityMapper, EntityMapper>();

            //Handlers


            return services;
        }
    }
}

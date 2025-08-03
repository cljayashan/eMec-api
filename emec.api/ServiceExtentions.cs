namespace emec.api
{
    public static class ServiceExtentions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register your custom services here  
            // Example: services.AddScoped<IMyService, MyService>();  

            // Add Authentication services  
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = configuration["Authentication:Authority"];
                    options.RequireHttpsMetadata = true;
                    options.Audience = configuration["Authentication:Audience"];
                });

            return services;
        }
    }
}

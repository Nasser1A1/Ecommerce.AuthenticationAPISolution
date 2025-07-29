using AuthenticationAPI.Application.Interfaces;
using AuthenticationAPI.Infrastructure.Data;
using AuthenticationAPI.Infrastructure.Repositories;
using eCommerce.SharedLib.DependencyIncjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthenticationAPI.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration config)
        {
            // Add dbcontext and repositories
            SharedServiceContainer.AddSharedServices<AuthenticationDbContext>(services, config, config["MySerilog:FileName"]!);
            services.AddScoped<IUser, UserRepository>();
            return services;
        }

        public static IApplicationBuilder UserInfrastructurePolicy(this IApplicationBuilder app)
        {
            // Add repositories here if needed
            // Example: services.AddScoped<IProductRepository, ProductRepository>();
            // Listen to api gateway only
            SharedServiceContainer.UseSharedPolicies(app);
            return app;
        }
    }
}

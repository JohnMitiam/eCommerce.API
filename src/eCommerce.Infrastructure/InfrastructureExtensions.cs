using eCommerce.Application.Interfaces.Data;
using eCommerce.Infrastructure.Data;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<DatabaseSession>();

        services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseInMemoryStorage());

        // Add the processing server as IHostedService
        services.AddHangfireServer();

        // Stored Procedure Repositories
        //services.AddTransient<IProductRepository, ProductRepository>();

        services.AddScoped<IUnitofWork, UnitofWork>();

        return services;

    }
}

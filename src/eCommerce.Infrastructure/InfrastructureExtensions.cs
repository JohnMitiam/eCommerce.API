using eCommerce.Application.Interfaces.Data;
using eCommerce.Infrastructure.Data;
using eCommerce.Infrastructure.Data.Repositories;
using eCommerce.Infrastructure.Data.StoredProcRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<DatabaseSession>();

        // Add the processing server as IHostedService

        // Inline SQL Repositories
        services.AddTransient<IProductRepository, ProductRepository>();

        // Stored Procedure Repositories
        //services.AddTransient<IProductRepository, SPProductRepository>();

        services.AddScoped<IUnitofWork, UnitofWork>();

        return services;

    }
}

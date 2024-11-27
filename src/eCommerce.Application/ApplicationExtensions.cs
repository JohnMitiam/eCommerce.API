using eCommerce.Application.Interfaces.Services;
using eCommerce.Application.Interfaces.Validator;
using eCommerce.Application.Mappings;
using eCommerce.Application.Services;
using eCommerce.Application.Validators.ProductValidators;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductValidator, ProductValidator>();

            return services;
        }
    }
}

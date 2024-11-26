using eCommerce.Application.ResourceParameters;
using eCommerce.Domain.Entities;

namespace eCommerce.Application.Interfaces.Data
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<(IEnumerable<Product> products, int recordCount)> GetAsync(ProductResourceParameters resourceParameters);
    }
}

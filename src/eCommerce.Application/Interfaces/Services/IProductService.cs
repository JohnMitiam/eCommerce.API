using eCommerce.Application.DTOs.Product;
using eCommerce.Application.ResourceParameters;
using eCommerce.Application.ResultModels;

namespace eCommerce.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<IServiceResult> GetByIdAsync(int productId);
        Task<IServiceResult> GetAsync(ProductResourceParameters resourceParameters);
        Task<IServiceResult> CreateAsync(CreateProductDTO product, string userId);
        Task<IServiceResult> UpdateAsync(int productId, UpdateProductDTO product, string userId);
        Task<IServiceResult> DeleteAsync(int productId, string userId);

    }
}

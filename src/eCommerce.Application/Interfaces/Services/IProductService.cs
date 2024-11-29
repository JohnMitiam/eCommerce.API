using eCommerce.Application.DTOs.Product;
using eCommerce.Application.ResourceParameters;
using eCommerce.Application.ResultModels;

namespace eCommerce.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<ViewProductDTO> GetByIdAsync(int productId);
        Task<PaginatedList<ViewProductDTO>> GetAsync(ProductResourceParameters resourceParameters);
        Task<ViewProductDTO> CreateAsync(CreateProductDTO product);
        Task<bool> UpdateAsync(int productId, UpdateProductDTO product);
        Task<bool> DeleteAsync(int productId);

    }
}

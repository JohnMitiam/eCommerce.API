using AutoMapper;
using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Interfaces.Data;
using eCommerce.Application.Interfaces.Services;
using eCommerce.Application.Interfaces.Validator;
using eCommerce.Application.ResourceParameters;
using eCommerce.Application.ResultModels;
using eCommerce.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace eCommerce.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductValidator _validator;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitofWork _UnitOfWork;

        public ProductService(IUnitofWork unitofwork, IMapper mapper, ILogger<ProductService> logger, IProductValidator validator)
        {
            _mapper = mapper;
            _logger = logger;
            _UnitOfWork = unitofwork;
            _validator = validator;
        }
        
        public async Task<ViewProductDTO?> CreateAsync(CreateProductDTO product)
        {
            try
            {
                var record = _mapper.Map<Product>(product);
                record.CreatedBy = "1";
                record.DateCreated = DateTime.UtcNow;

                await _validator.IsValidAsync(record);

                _UnitOfWork.CreateTransaction();

                await _UnitOfWork.Products.CreateAsync(record);

                _UnitOfWork.Commit();

                return _mapper.Map<ViewProductDTO>(record);

            }
            catch (Exception ex)
            {
                _UnitOfWork.Rollback();
                _logger.LogError($@"{ex.Message}");
            }

            return null;
        }

        public async Task<bool> DeleteAsync(int productId)
        {
            try
            {
                var record = await _UnitOfWork.Products.GetByIdAsync(productId);
                if (record == null)
                {
                    return false;
                }

                _UnitOfWork.CreateTransaction();
                await _UnitOfWork.Products.DeleteAsync(record.Id);
                _UnitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                _UnitOfWork.Rollback();
                _logger.LogError($@"{ex.Message}");
            }

            return false;
        }

        public async Task<PaginatedList<ViewProductDTO>> GetAsync(ProductResourceParameters resourceParameters)
        {
            resourceParameters.SearchFields = new List<string> { "Name", "Description" };
            var result = await _UnitOfWork.Products.GetAsync(resourceParameters).ConfigureAwait(false);

            var paginatedResult = new PaginatedList<ViewProductDTO>(
                _mapper.Map<IEnumerable<ViewProductDTO>>(result.products).ToList(),
                result.recordCount,
                resourceParameters.Page,
                resourceParameters.PageSize);

            return paginatedResult;
        }

        public async Task<ViewProductDTO?> GetByIdAsync(int productId)
        {
            var record = await _UnitOfWork.Products.GetByIdAsync(productId).ConfigureAwait(false);

            return _mapper.Map<ViewProductDTO>(record);
        }

        public async Task<bool> UpdateAsync(int productId, UpdateProductDTO product)
        {
            try
            {
                var record = await _UnitOfWork.Products.GetByIdAsync(productId).ConfigureAwait(false);
                if (record == null)
                    return false;

                _mapper.Map(product, record);

                _UnitOfWork.CreateTransaction();
                await _UnitOfWork.Products.UpdateAsync(record).ConfigureAwait(false);
                _UnitOfWork.Commit();

                return true;
            }
            catch (Exception ex)
            {
                _UnitOfWork.Rollback();
                _logger.LogError($@"{ex.Message}");
                return false;
            }
        }
    }
}

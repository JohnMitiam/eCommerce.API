using AutoMapper;
using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Interfaces.Data;
using eCommerce.Application.Interfaces.Services;
using eCommerce.Application.Interfaces.Validator;
using eCommerce.Application.ResourceParameters;
using eCommerce.Application.ResultModels;
using eCommerce.Application.Services.Base;
using eCommerce.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace eCommerce.Application.Services
{
    public class ProductService : BaseService, IProductService
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
        public async Task<IServiceResult> CreateAsync(CreateProductDTO product, string userId)
        {
            try
            {
                var record = _mapper.Map<Product>(product);
                record.CreatedBy = userId;
                record.DateCreated = DateTime.UtcNow;

                var validationResult = _validator.IsValid(record);
                if (!validationResult.isSuccess)
                {
                    var errorMessage = validationResult.errorMessages != null ? string.Join(". ", validationResult.errorMessages) : "Validation failed";
                    return FailedResult(errorMessage);
                }

                _UnitOfWork.CreateTransaction();

                await _UnitOfWork.Products.CreateAsync(record);

                _UnitOfWork.Commit();

                return SuccessResult(_mapper.Map<ViewProductDTO>(record));

            }
            catch (Exception ex)
            {
                _UnitOfWork.Rollback();
                _logger.LogError($@"{ex.Message}");
            }

            return FailedResult("An error occured while processing your request.");
        }

        public async Task<IServiceResult> DeleteAsync(int currencyId, string userId)
        {
            try
            {
                var record = await _UnitOfWork.Products.GetByIdAsync(currencyId);
                if (record == null)
                {
                    return FailedResult(ServiceConstants.RecordNotFound);
                }

                record.UpdatedBy = userId;
                record.DateUpdated = DateTime.UtcNow;

                _UnitOfWork.CreateTransaction();
                await _UnitOfWork.Products.DeleteAsync(record.Id);
                _UnitOfWork.Commit();

                return SuccessResult();
            }
            catch (Exception ex)
            {
                _UnitOfWork.Rollback();
                _logger.LogError($@"{ex.Message}");

                return FailedResult("An error occured while processing your request.");
            }
        }

        public async Task<IServiceResult> GetAsync(ProductResourceParameters resourceParameters)
        {
            var result = await _UnitOfWork.Products.GetAsync(resourceParameters).ConfigureAwait(false);

            var paginatedResult = new PaginatedList<ViewProductDTO>(
                _mapper.Map<IEnumerable<ViewProductDTO>>(result.products).ToList(),
                result.recordCount,
                resourceParameters.Page,
                resourceParameters.PageSize);

            return SuccessResult(paginatedResult);
        }

        public async Task<IServiceResult> GetByIdAsync(int currencyId)
        {
            try
            {
                var record = await _UnitOfWork.Products.GetByIdAsync(currencyId).ConfigureAwait(false);

                return SuccessResult(_mapper.Map<ViewProductDTO>(record));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $@"{ex.Message}");

                return FailedResult("An error occured while processing your request.");
            }
        }

        public async Task<IServiceResult> UpdateAsync(int productId, UpdateProductDTO product, string userId)
        {
            try
            {
                var record = await _UnitOfWork.Products.GetByIdAsync(productId).ConfigureAwait(false);
                if (record == null)
                    return FailedResult(ServiceConstants.RecordNotFound);

                _mapper.Map(product, record);


                var validationResult = _validator.IsValid(record);
                if (!validationResult.isSuccess)
                {
                    var errorMessage = validationResult.errorMessages != null ? string.Join(". ", validationResult.errorMessages) : "Validation failed";
                    return FailedResult(errorMessage);
                }

                _UnitOfWork.CreateTransaction();
                await _UnitOfWork.Products.UpdateAsync(record).ConfigureAwait(false);
                _UnitOfWork.Commit();

                return SuccessResult();
            }
            catch (Exception ex)
            {
                _UnitOfWork.Rollback();
                _logger.LogError($@"{ex.Message}");

                return FailedResult("An error occured while processing your request.");
            }
        }
    }
}

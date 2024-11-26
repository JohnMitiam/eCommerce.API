using eCommerce.API.Extensions;
using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Interfaces.Services;
using eCommerce.Application.ResourceParameters;
using eCommerce.Application.ResultModels;
using eCommerce.Application.Services.Base;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _product;
        private readonly string _userId = "1";
        private IServiceResult ServiceResult { get; set; } = null!;
        public ProductController(IProductService product)
        {
            _product = product;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync([FromQuery] ProductResourceParameters resourceParameters)
        {
            ServiceResult = await _product.GetAsync(resourceParameters);

            if (ServiceResult.IsSuccess)
            {
                var result = ServiceResult as ServiceResult<PaginatedList<ViewProductDTO>>;

                if (result != null)
                {
                    return Ok(new
                    {
                        data = result.Data,
                        total = result.Data.TotalCount,
                        page = result.Data.Page,
                        pageSize = result.Data.PageSize,
                        totalPages = result.Data.TotalPages,
                    });
                }
            }

            return BadRequest(ServiceResult);
        }

        [HttpGet("{id}", Name = nameof(ProductController.GetProductsAsync))]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            ServiceResult = await _product.GetByIdAsync(id);

            if (ServiceResult.IsSuccess)
            {
                var result = ServiceResult as ServiceResult<ViewProductDTO>;
                if (result != null)
                {
                    return Ok(result);
                }
            }

            if (ServiceResult.ErrorMessage == ServiceConstants.RecordNotFound)
            {
                return NotFound();
            }

            return BadRequest(ServiceResult);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCurrencyAsync([FromBody] CreateProductDTO product)
        {
            var userId = User.GetUserId();

            ServiceResult = await _product.CreateAsync(product, userId);

            if (ServiceResult.IsSuccess)
            {
                var result = ServiceResult as ServiceResult<ViewProductDTO>;

                if (result != null)
                    return CreatedAtRoute(nameof(ProductController.GetProductByIdAsync), new { id = result.Data.Id }, result);
            }

            return BadRequest(ServiceResult);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCurrencyAsync([FromRoute] int id, [FromBody] UpdateProductDTO product)
        {
            if (id == 0 || id != product.Id)
            {
                return BadRequest();
            }

            var userId = User.GetUserId();

            ServiceResult = await _product.UpdateAsync(id, product, userId);

            if (ServiceResult.IsSuccess)
            {
                return NoContent();
            }

            if (ServiceResult.ErrorMessage == ServiceConstants.RecordNotFound)
            {
                return NotFound();
            }

            return BadRequest(ServiceResult);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrencyAsync([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var userId = User.GetUserId();

            ServiceResult = await _product.DeleteAsync(id, userId);

            if (ServiceResult.IsSuccess)
                return NoContent();

            if (ServiceResult.ErrorMessage == ServiceConstants.RecordNotFound)
            {
                return NotFound();
            }

            return BadRequest(ServiceResult);

        }
    }
}

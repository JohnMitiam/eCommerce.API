using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Interfaces.Services;
using eCommerce.Application.ResourceParameters;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _product;
        public ProductController(IProductService product)
        {
            _product = product;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync(string? search, int page=1, int pagesize=10)
        {
            var resourceParameters = new ProductResourceParameters
            {
                Search = search,
                PageSize = pagesize,
                Page = page,
            };
            var records = await _product.GetAsync(resourceParameters);

            return Ok(new
            {
                data = records,
                total = records.TotalCount,
                page = resourceParameters.Page,
                pagesize = resourceParameters.PageSize,
                totalPages = records.TotalPages,
            });
        }

        [HttpGet("{id}", Name = nameof(ProductController.GetProductByIdAsync))]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var product = await _product.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync([FromBody] CreateProductDTO product)
        {
            var record = await _product.CreateAsync(product);

            if (record == null)
                return BadRequest("Failed");

            return CreatedAtRoute(nameof(ProductController.GetProductByIdAsync), new { id = record.Id }, record);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync([FromRoute] int id, [FromBody] UpdateProductDTO product)
        {
            var isSuccess = await _product.UpdateAsync(id, product);

            if (!isSuccess)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var product = await _product.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var isSuccess = await _product.DeleteAsync(id);

            if (!isSuccess)
                return NotFound();

            return NoContent();

        }
    }
}

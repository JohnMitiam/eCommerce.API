//using AutoMapper;
//using eCommerce.Application.DTOs.Product;
//using eCommerce.Application.Interfaces.Services;
//using eCommerce.Application.Mappings;
//using eCommerce.Application.ResultModels;
//using eCommerce.Application.Services;
//using eCommerce.Application.Test.Data;
//using eCommerce.Application.Validators.ProductValidators;
//using Microsoft.Extensions.Logging;
//using Moq;

//namespace eCommerce.Application.Test
//{
//    public class ProductServiceTest : IDisposable
//    {
//        private readonly IProductService _product;
//        public ProductServiceTest()
//        {
//            var mockDataFactory = new MockDataFactory();
//            var mockLogger = new Mock<ILogger<ProductService>>();
//            var mockProductValidator = new Mock<ProductValidator>();
//            var mapperConfiguration = new MapperConfiguration(config =>
//            {
//                config.AddProfile<MappingProfile>();
//            });

//            _product = new ProductService(
//                mockDataFactory.UnitofWork,
//                mapperConfiguration.CreateMapper(),
//                mockLogger.Object,
//                mockProductValidator.Object
//                );
//        }

//        [Fact]
//        public async Task Given_ValidProduct_CreateProduct_Returns_Success()
//        {
//            // Arrange
//            var userId = "unit_test";

//            var product = new CreateProductDTO();
//            product.Name = "New Product";
//            product.Description = "Product Description";
//            product.Price = 100;
//            product.ItemStocks = 1000;

//            // Act
//            var result = await _product.CreateAsync(product, userId) as ServiceResult<ViewProductDTO>;

//            // Assert
//            Assert.True(result.IsSuccess);

//            Assert.Equal(result.Data.Name, product.Name);
//            Assert.Equal(result.Data.Description, product.Description);
//            Assert.Equal(result.Data.Price, product.Price);
//            Assert.Equal(result.Data.ItemStocks, product.ItemStocks);
//        }

//        [Fact]
//        public async Task Given_ValidProduct_UpdateProduct_Returns_Success()
//        {
//            // Arrange
//            var userId = "unit_test";

//            var product = new CreateProductDTO();
//            product.Name = "New Product";
//            product.Description = "Product Description";
//            product.Price = 100;
//            product.ItemStocks = 1000;

//            var updateProduct = new UpdateProductDTO();
//            updateProduct.Name = "New Product Updaed";
//            updateProduct.Description = "Product Description Updated";
//            updateProduct.Price = 101;
//            updateProduct.ItemStocks = 1001;

//            // Act
//            var createdResult = await _product.CreateAsync(product, userId) as ServiceResult<ViewProductDTO>;

//            var productId = createdResult?.Data.Id;
//            updateProduct.Id = productId.Value;

//            var updatedResult = await _product.UpdateAsync(productId.Value, updateProduct, userId);

//            var getUpdatedResult = await _product.GetByIdAsync(productId.Value) as ServiceResult<ViewProductDTO>;

//            // Assert
//            Assert.True(createdResult.IsSuccess);
//            Assert.True(updatedResult.IsSuccess);
//            Assert.True(getUpdatedResult.IsSuccess);

//            // Create
//            Assert.Equal(createdResult.Data.Name, product.Name);
//            Assert.Equal(createdResult.Data.Description, product.Description);
//            Assert.Equal(createdResult.Data.Price, product.Price);
//            Assert.Equal(createdResult.Data.ItemStocks, product.ItemStocks);

//            // Updated
//            Assert.Equal(updateProduct.Name, getUpdatedResult.Data.Name);
//            Assert.Equal(updateProduct.Description, getUpdatedResult.Data.Description);
//            Assert.Equal(updateProduct.Price, getUpdatedResult.Data.Price);
//            Assert.Equal(updateProduct.ItemStocks, getUpdatedResult.Data.ItemStocks);
//        }

//        [Fact]
//        public async Task Given_ValidProduct_DeleteProduct_Returns_Success()
//        {
//            // Arrange
//            var userId = "unit_test";

//            var product = new CreateProductDTO
//            {
//                Name = "New Product",
//                Description = "Product Description",
//                Price = 100,
//                ItemStocks = 1000,
//            };

//            // Act          
//            var createResult = await _product.CreateAsync(product, userId) as ServiceResult<ViewProductDTO>;

//            var productId = createResult?.Data.Id;

//            var deleteResult = await _product.DeleteAsync(productId.Value, userId);

//            // Assert
//            Assert.True(createResult.IsSuccess);
//            Assert.True(deleteResult.IsSuccess);
//        }

//        public void Dispose()
//        {
//            // Clear Test Data
//        }
//    }
//}

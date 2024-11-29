using Dapper;
using eCommerce.Application.Interfaces.Data;
using eCommerce.Application.ResourceParameters;
using eCommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure.Data.StoredProcRepositories
{
    public class SPProductRepository : IProductRepository
    {
        private readonly DatabaseSession _dbSession;

        public SPProductRepository(DatabaseSession dbSession)
        {
            _dbSession = dbSession;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            var query = $@"INSERT INTO Organization (Name, Description, ItemStock, Price, IsActive, IsDeleted, CreatedBy, DateCreated)                            
                            VALUES (@Name, Description, ItemStock, Price, IsActive, 0, @CreatedBy, @DateCreated);
                            SELECT LAST_INSERT_ID();
                            ";

            var queryParams = new
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ItemStocks = product.ItemStocks,
                IsActive = product.IsActive,
                CreatedBy = product.CreatedBy,
                DateCreated = product.DateCreated,
            };

            product.Id = await _dbSession.Connection.ExecuteScalarAsync<int>(query, queryParams, _dbSession.Transaction, commandType: CommandType.StoredProcedure);

            return product;
        }

        public async Task<bool> DeleteAsync(int productId)
        {
            var query = $@"sp_DeleteProduct;";

            var queryParams = new
            {
                ProductId = productId
            };

            await _dbSession.Connection.ExecuteAsync(query, queryParams, _dbSession.Transaction, commandType: CommandType.StoredProcedure);

            return true;
        }

        public async Task<(IEnumerable<Product> products, int recordCount)> GetAsync(ProductResourceParameters resourceParameters)
        {
            IEnumerable<Product> result;

            var dataQuery = $@"SELECT * FROM Product WHERE IsDeleted=0 ";
            var dataCountQuery = $@"SELECT COUNT(*) FROM Product WHERE IsDeleted=0 ";

            var queryParamBuilder = new QueryParameters(resourceParameters.Search, resourceParameters.SearchFields, resourceParameters.Page, resourceParameters.PageSize);

            var searchSQLQuery = queryParamBuilder.GetSearchSQLQuery();
            dataQuery += searchSQLQuery;
            dataCountQuery += searchSQLQuery;

            var filterSQLQuery = queryParamBuilder.GetFilterSQLQuery();
            dataQuery += filterSQLQuery;
            dataCountQuery += filterSQLQuery;

            dataQuery += queryParamBuilder.GetPaginationSQLQuery();

            result = await _dbSession.Connection.QueryAsync<Product>(dataQuery, queryParamBuilder.Parameters);
            var totalCount = await _dbSession.Connection.ExecuteScalarAsync<int>(dataCountQuery, queryParamBuilder.Parameters);

            return (result, totalCount);
        }

        public Task<IEnumerable<Product>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Product?> GetByIdAsync(int productId)
        {
            var query = $@"sp_GetProductById;";
            var queryParams = new
            {
                ProductId = productId
            };

            var result = await _dbSession.Connection.QueryFirstOrDefaultAsync<Product>(query, queryParams, commandType: CommandType.StoredProcedure);

            return result;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            var query = $@"UPDATE Product SET Name = @Name, Description = @Description, Price = @Price, ItemStocks = @ItemStocks, IsActive = @IsActive
                            WHERE IsDeleted=0 AND ID=@ID;";
            var queryParams = new
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                ItemStocks = product.ItemStocks,
                IsActive = product.IsActive,
                CreatedBy = product.CreatedBy,
                DateCreated = product.DateCreated,
                ProductId = product.Id
            };

            await _dbSession.Connection.ExecuteAsync(query, queryParams, _dbSession.Transaction, commandType: CommandType.StoredProcedure);

            return true;
        }
    }
}

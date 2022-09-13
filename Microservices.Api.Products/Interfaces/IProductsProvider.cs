using Microservices.Api.Products.Db;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microservices.Api.Products.Interfaces
{
    public interface IProductsProvider
    {
        public Task<(bool IsSuccess, IEnumerable<Product> products, string ErrorMessage)> GetProductsAsync();
        public Task<(bool IsSuccess, Product Product, string ErrorMessage)> GetProductAsync(int id);
    }
}

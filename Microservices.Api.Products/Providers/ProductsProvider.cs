using AutoMapper;
using Microservices.Api.Products.Db;
using Microservices.Api.Products.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;
        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Product()
                {
                    Id = 1,
                    Name = "Keyboard",
                    Price = 20,
                    Inventory = 10
                });
                dbContext.Products.Add(new Product()
                {
                    Id = 2,
                    Name = "Headphone",
                    Price = 20,
                    Inventory = 10
                });
                dbContext.Products.Add(new Product()
                {
                    Id = 3,
                    Name = "Monitor",
                    Price = 20,
                    Inventory = 60
                });
                dbContext.Products.Add(new Product()
                {
                    Id = 4,
                    Name = "Motherboard",
                    Price = 2000,
                    Inventory = 50
                });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Product> products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                logger?.LogInformation("Querying products");
                var products = await dbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    logger?.LogInformation($"{products.Count} product(s) found");
                    var result = mapper.Map<IEnumerable<Product>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                logger?.LogInformation($"Querying products with id: {id}");
                var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    logger?.LogInformation("Product found");
                    var result = mapper.Map<Product>(product);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}

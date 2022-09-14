using AutoMapper;
using Microservices.Api.Products.Db;
using Microservices.Api.Products.Profiles;
using Microservices.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Microservices.Api.Products.Tests
{
    public class ProductsServiceTest
    {

        [Fact]
        public void Test()
        {
            Assert.True(1 == 1);
        }

        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
              .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
              .Options;

            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext,
                null, mapper);
            var products = await productsProvider.GetProductsAsync();
            Assert.True(products.IsSuccess);
            Assert.True(products.products.Any());
            Assert.Null(products.ErrorMessage);
        }

        [Fact]
        public async Task GetProductReturnsProductUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
              .UseInMemoryDatabase(nameof(GetProductReturnsProductUsingValidId))
              .Options;

            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext,
                null, mapper);
            var product = await productsProvider.GetProductAsync(2);
            Assert.True(product.IsSuccess);
            Assert.True(product.Product.Id == 2);
            Assert.Null(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductReturnsProductUsingInvalidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
              .UseInMemoryDatabase(nameof(GetProductReturnsProductUsingInvalidId))
              .Options;

            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext,
                null, mapper);
            var product = await productsProvider.GetProductAsync(-10);
            Assert.False(product.IsSuccess);
            Assert.Null(product.Product);
            Assert.NotNull(product.ErrorMessage);
        }

        private void CreateProducts(ProductsDbContext dbContext)
        {
            for (int i = 1; i <= 10; i++)
            {
                dbContext.Products.Add(new Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i * 10,
                    Price = (decimal)(i * 15)
                });
            }
            dbContext.SaveChanges();
        }
    }
}
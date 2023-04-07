using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Configuration;
using P3AddNewFunctionalityDotNetCore.Data;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Models;

using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using Xunit;

using Microsoft.AspNetCore.Mvc;


namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class IntegrationTests
    {
        private readonly IStringLocalizer<ProductService> _localizer;
        private readonly IConfiguration _configuration;

        [Fact]
        public void SaveNewProductToDbTest()
        {
            var options = new DbContextOptionsBuilder<P3Referential>()
                 //UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=P3Referential-2f561d3b-493f-46fd-83c9-6e2643e7bd0a;Trusted_Connection=True;MultipleActiveResultSets=true")
                 .UseSqlServer("Server=.;Database=P3Referential-2f561d3b-493f-46fd-83c9-6e2643e7bd0a;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            var ctx = new P3Referential(options, _configuration);

            var cart = new Cart();
            var productRepository = new ProductRepository(ctx);
            var orderRepository = new OrderRepository(ctx);
            var languageService = new LanguageService();

            //ProductController productController = null;

            ProductController productController = new ProductController(new ProductService(cart, productRepository, orderRepository, _localizer), languageService);
            // act 
            var product = new ProductViewModel { Name = "Lucien", Price = "100", Stock = "12", Details = "info", Description = "pp" };

            var result = productController.Create(product) as ViewResult;

            Assert.Null(result);
            // Vérifier que le résultat est nul
           
        }

        [Fact]
        public void DeleteProductFromDbTest()
        {
            // Setup
            var options = new DbContextOptionsBuilder<P3Referential>()
                .UseSqlServer("Server=.;Database=P3Referential-2f561d3b-493f-46fd-83c9-6e2643e7bd0a;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            var ctx = new P3Referential(options, _configuration);

            var cart = new Cart();
            var productRepository = new ProductRepository(ctx);
            var orderRepository = new OrderRepository(ctx);
            var languageService = new LanguageService();

            ProductController productController = new ProductController(new ProductService(cart, productRepository, orderRepository, _localizer), languageService);
            ProductService productService = new ProductService(cart, productRepository, orderRepository, _localizer);


            var totalProduct = productService.GetAllProducts();
            var lastId = totalProduct.Max(p => p.Id);
            productController.DeleteProduct(lastId);

            // Assert
            Assert.DoesNotContain(productRepository.GetAllProducts(), p => p.Id == lastId);

        }


    }
}
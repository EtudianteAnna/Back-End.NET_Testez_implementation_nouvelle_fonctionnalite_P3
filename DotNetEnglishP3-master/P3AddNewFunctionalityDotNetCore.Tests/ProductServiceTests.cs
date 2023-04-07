using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Moq;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;


namespace P3AddNewFunctionalityDotNetCore.Tests
{

    public class ProductServiceTests
    {

        /// <summary>
        /// Take this test method as a template to write your test method.
        /// A test method must check if a definite method does its job:
        /// returns an expected value from a particular set of parameters
        /// </summary>
        [Fact]
        public void Given_A_Name_When_Missing_Name_ThenThrowError()
        {
            // Arrange
            var languageService = Mock.Of<ILanguageService>();
            var cart = Mock.Of<ICart>();
            var productRepository = Mock.Of<IProductRepository>();
            var orderRepository = Mock.Of<IOrderRepository>();
            var localizer = Mock.Of<IStringLocalizer<ProductService>>();
            var productController = new ProductController(new ProductService(cart, productRepository, orderRepository, localizer), languageService);
            var product = new ProductViewModel
            {
                Name = "",
            };
            // Act
            var result = productController.Create(product) as ViewResult;
            var modelStateValues = result.ViewData.ModelState.Values;

            // Assert
            Assert.Contains(modelStateValues.FirstOrDefault().Errors, item => item.ErrorMessage == Resources.ProductService.MissingName);
        }
        [Fact]
        public void Given_Price_When_Not_ThenThrowError()
        {
            // Arrange
            var languageService = Mock.Of<ILanguageService>();
            var cart = Mock.Of<ICart>();
            var productRepository = Mock.Of<IProductRepository>();
            var orderRepository = Mock.Of<IOrderRepository>();
            var localizer = Mock.Of<IStringLocalizer<ProductService>>();
            var productController = new ProductController(new ProductService(cart, productRepository, orderRepository, localizer), languageService);
            var product = new ProductViewModel
            {
                //  Name = "Nom facile",
                Price = "",
                // Stock = "2",
            };

            // Act
            var result = productController.Create(product) as ViewResult;
            var modelStateValues = result.ViewData.ModelState.Values;

            // int indice = expected.IndexOf("The Price field is required");

            //Assert

            Assert.Contains(modelStateValues.FirstOrDefault().Errors, item => item.ErrorMessage == Resources.ProductService.MissingPrice);
        }
        [Fact]
        public void Given_Number_For_Price_Then_Throw_Error()
        {
            // Arrange
            var languageService = Mock.Of<ILanguageService>();
            var cart = Mock.Of<ICart>();
            var productRepository = Mock.Of<IProductRepository>();
            var orderRepository = Mock.Of<IOrderRepository>();
            var localizer = Mock.Of<IStringLocalizer<ProductService>>();
            var productController = new ProductController(new ProductService(cart, productRepository, orderRepository, localizer), languageService);
            var product = new ProductViewModel
            {
                Name = "Nom facile",
                Price = "P",
                Stock = "2",
            };

            // Act
            var result = productController.Create(product) as ViewResult;
            var modelStateValues = result.ViewData.ModelState.Values;

            //Assert
            Assert.Equal(2, modelStateValues.FirstOrDefault().Errors.Count);
            Assert.Contains(modelStateValues.FirstOrDefault().Errors, item => item.ErrorMessage == Resources.ProductService.PriceNotANumber);
            Assert.Contains(modelStateValues.FirstOrDefault().Errors, item => item.ErrorMessage == Resources.ProductService.PriceNotGreaterThanZero);
        }
        [Fact]
        public void Given_Product_When_PriceEqualToZero_Then_Error()
        {
            // Arrange
            var languageService = Mock.Of<ILanguageService>();
            var cart = Mock.Of<ICart>();
            var productRepository = Mock.Of<IProductRepository>();
            var orderRepository = Mock.Of<IOrderRepository>();
            var localizer = Mock.Of<IStringLocalizer<ProductService>>();
            var productController = new ProductController(new ProductService(cart, productRepository, orderRepository, localizer), languageService);
            var product = new ProductViewModel
            {
                Name = "Nom facile",
                Price = "0",
                Stock = "2",
            };

            // Act
            var result = productController.Create(product) as ViewResult;
            var modelStateValues = result.ViewData.ModelState.Values;

            //Assert
            Assert.Contains(modelStateValues.FirstOrDefault().Errors, item => item.ErrorMessage == Resources.ProductService.PriceNotGreaterThanZero);
        }

        [Fact]
        public void Given_Stock_WhenNot_ThenThrowError()
        {
            // Arrange
            var languageService = Mock.Of<ILanguageService>();
            var cart = Mock.Of<ICart>();
            var productRepository = Mock.Of<IProductRepository>();
            var orderRepository = Mock.Of<IOrderRepository>();
            var localizer = Mock.Of<IStringLocalizer<ProductService>>();
            var productController = new ProductController(new ProductService(cart, productRepository, orderRepository, localizer), languageService);
            var product = new ProductViewModel
            {
                //Name = "Nom facile",
                // Price = "200",
                Stock = "",
            };

            // Act
            var result = productController.Create(product) as ViewResult;
            var modelStateValues = result.ViewData.ModelState.Values;

            //Assert
            Assert.Contains(modelStateValues.FirstOrDefault().Errors, item => item.ErrorMessage == Resources.ProductService.MissingStock);

        }

        [Fact]
        public void Given_AnIntegerForStock_When_Not_ThenThrowError()
        {
            // Arrange
            var languageService = Mock.Of<ILanguageService>();
            var cart = Mock.Of<ICart>();
            var productRepository = Mock.Of<IProductRepository>();
            var orderRepository = Mock.Of<IOrderRepository>();
            var localizer = Mock.Of<IStringLocalizer<ProductService>>();
            var productController = new ProductController(new ProductService(cart, productRepository, orderRepository, localizer), languageService);
            var product = new ProductViewModel
            {
                // Name = "Nom facile",
                // Price = "200",
                Stock = "S",
            };

            // Act
            var result = productController.Create(product) as ViewResult;
            var modelStateValues = result.ViewData.ModelState.Values;
            List<string> expected = new List<string>();
            //Assert
            Assert.Contains(modelStateValues.FirstOrDefault().Errors, item => item.ErrorMessage == Resources.ProductService.StockNotAnInteger);
        }
    }
}
// TODO write test methods to ensure a correct coverage of all possibilities




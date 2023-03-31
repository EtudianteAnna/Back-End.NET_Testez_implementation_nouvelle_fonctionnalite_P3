﻿using Xunit;
using Moq;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using Microsoft.Extensions.Localization;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System;
using Microsoft.AspNetCore.JsonPatch.Internal;
using P3AddNewFunctionalityDotNetCore.Models.Entities;

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
        public void CreerUnObjetVideTest()
        {
            // Arrange
            var langageService = Mock.Of<ILanguageService>();
            var cart = Mock.Of<Cart>();
            var productRepository = Mock.Of<ProductRepository>();
            var orderRepository = Mock.Of<IOrderRepository>();
            var localizer = Mock.Of<IStringLocalizer<ProductService>>();
            var productController = new ProductController(new ProductService(cart, productRepository, orderRepository, localizer), langageService);
            var product = new ProductViewModel()

            {
                Name = string.Empty,
                Price = string.Empty,
                Description = string.Empty,
                Stock = string.Empty,
                Details = string.Empty,

            };


            // Act
            var result = productController.Create(product) as ViewResult;

            // Assert
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]

        public void CreerUnObjetPleinTest()


        {
            // Arrange
            var languageService = Mock.Of<ILanguageService>();
            var cart = Mock.Of<ICart>();
            var productRepository = Mock.Of<IProductRepository>();
            var orderRepository = Mock.Of<IOrderRepository>();
            var localizer = Mock.Of<IStringLocalizer<ProductService>>();
            var productController = new ProductController(new ProductService(cart, productRepository, orderRepository, localizer), languageService);
            var product = new ProductViewModel()
            {

                Name = "NomFacile",
                Price = "100",
                Stock = "2",
                Details = "TresFacile",

            };

            //Act

            var result = productController.Create(product) as ViewResult;

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void CreerUnNomDobjetTest()
        {

            //Arrange

            var languageService = Mock.Of<ILanguageService>();
            var cart = Mock.Of<ICart>();
            var productRepository = Mock.Of<IProductRepository>();
            var orderRepository = Mock.Of<IOrderRepository>();
            var localizer = Mock.Of<IStringLocalizer<ProductService>>();
            var productController = new ProductController(new ProductService(cart, productRepository, orderRepository, localizer), languageService);
            var product = new ProductViewModel()

            {
                Name = string.Empty,
                Price = "100",
                Stock = "2",
                Details = " ObjetSimple",
            };

            CultureInfo.CurrentCulture = new CultureInfo("en-GB");
            //Act
            var result = productController.Create(product) as ViewResult;
            var modelStateValues = result.ViewData.ModelState.Values;
            List<string> errors = new List<string>();
            foreach (var error in modelStateValues)
            {
                foreach (var value in error.Errors)
                {
                    errors.Add(value.ErrorMessage);
                }

            }

            //Assert
            Assert.False(result.ViewData.ModelState.IsValid);
            Assert.Single(errors);
            Assert.Equal("Please enter a name", errors.First());
            Assert.True(errors.Count == 1 && errors.First() == "Please enter a name");

        }

        [Fact]
        public void CreerNomDobjetVideTest()
        {

        //Arrange
        var languageService = Mock.Of<ILanguageService>();
        var cart = Mock.Of<Cart>();
        var productRepository = Mock.Of<ProductRepository>();
        var orderRepository = Mock.Of<OrderRepository>();
        var localizer = Mock.Of<IStringLocalizer<ProductService>>();
        var productController = new ProductController(new ProductService(cart, productRepository, orderRepository, localizer), languageService);
            var product = new ProductViewModel()
            {

                Name = string.Empty,
                Price = "100",
                Description = "UnObjetSimple",
                Stock = "2",
                Details = "TresSimple",

            };

            CultureInfo.CurrentCulture = new CultureInfo("fr-FR");
        
        //Act
        var result=productController.Create(product) as ViewResult;
            var ModelStateValues = result.ViewData.ModelState.Values;
            List<string> errors = new List<string>();
            foreach(var error in ModelStateValues)
            {
                //je ne suis pas sure 
              foreach (var err in error.Errors)
                {

                    errors.Add(err.ErrorMessage); 
                }

            }
        

        //Assert

        Assert.False(result.ViewData.ModelState.IsValid);
        Assert.Single(errors);
        Assert.Equal("Veuillez saisir un prix", errors.First());

        }

    [Fact]
    public void CreerUnPrixNegatifDeLobjetTest()
    {
        // Arrange
        var languageService = Mock.Of<ILanguageService>();
        var cart = Mock.Of<ICart>();
        var productRepository = Mock.Of<IProductRepository>();
        var orderRepository = Mock.Of<IOrderRepository>();
        var localizer = Mock.Of<IStringLocalizer<ProductService>>();
        var productController = new ProductController(new ProductService(cart, productRepository, orderRepository, localizer), languageService);
        var product = new ProductViewModel()
        {

            Name = "NomFacile",
            Price = "-5",
            Description = "UnObjetSimple",
            Stock = "1",
            Details = "TresSimple",
        };

        CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
        //Act
        var result = productController.Create(product) as ViewResult;
        var modelStateValues = result.ViewData.ModelState.Values;
        List<string> errors = new List<string>();
        foreach (var result in modelStateValues) 
        
        {
        foreach (var error in result.Errors)
            {
                errors.Add(error.ErrorMessage);
            
            }
        
        }
        //Assert

        Assert.False(result.ViewData.ModelState.IsValid);
        Assert.Equal("Le prix doit être supérieur à zéro",errors[0]);
        Assert.Contains("Le prix doit être supérieur à zéro" ,errors);

    }
    [Fact]
    public void CreerNegativePriceObjectTest()
    {
        
            // Arrange
            var languageService = Mock.Of<ILanguageService>();
            var cart = Mock.Of<ICart>();
            var productRepository = Mock.Of<IProductRepository>();
            var orderRepository = Mock.Of<IOrderRepository>();
            var localizer = Mock.Of<IStringLocalizer<ProductService>>();
            var productController = new ProductController(new ProductService(cart, productRepository, orderRepository, localizer), languageService);
            var product = new ProductViewModel()
            {

                Name = "NomFacile",
                Price = "-5",
                Description = "UnObjetSimple",
                Stock = "1",
                Details = "TresSimple",
            };

            CultureInfo.CurrentUICulture = new CultureInfo("en-GB");
            //Act
            var result = productController.Create(product) as ViewResult;
            var modelStateValues = result.ViewData.ModelState.Values;
            List<string> errors = new List<string>();
            foreach (var result in modelStateValues)

            {
                foreach (var error in result.Errors)
                {
                    errors.Add(error.ErrorMessage);

                }

            }
            //Assert

            Assert.False(result.ViewData.ModelState.IsValid);
            Assert.Equal("The price should be greater than zéro", errors[0]);
            Assert.Contains("The price should be greater than zero", errors);





        }







        [Fact]
    public void TestDeGetProduct()
    {
        var languageService = Mock.Of<ILanguageService>();
        var cart = Mock.Of<ICart>();
        var productRepository = Mock.Of<IProductRepository>();
        var orderRepository = Mock.Of<IOrderRepository>();
        var localizer = Mock.Of<IStringLocalizer<ProductService>>();
        var productService = new ProductService(cart, productRepository, orderRepository, localizer);
        var id = 12;
        var productExpected = new Product();
        var product = productService.GetProduct(id);


    }
}
}

        // TODO write test methods to ensure a correct coverage of all possibilities


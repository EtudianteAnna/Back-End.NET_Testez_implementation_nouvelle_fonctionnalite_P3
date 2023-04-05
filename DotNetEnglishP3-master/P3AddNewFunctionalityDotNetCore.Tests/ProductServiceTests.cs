using Xunit;
using Moq;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using Microsoft.Extensions.Localization;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System;


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

        public void ShouldMissingProductName_Test()
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
                //voir avec Hanna
                Name = string.Empty,
                // Price = "200",
                // Stock = "2",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("en-GB");
            // Act

            var result = productController.Create(product) as ViewResult;
            var values = result.ViewData.ModelState.Values;
            List<string> expected = new List<string>();
            foreach (var res in values)
            {
                foreach (var err in res.Errors)
                {
                    expected.Add(err.ErrorMessage);
                    Console.WriteLine(expected);
                }
            }
            // Assert
            Assert.Equal("Please enter a name of product", expected.First());
        }

        [Fact]
        public void ShouldSaisirUnNomDeProduit_Test()
        {
            // Arrange
            var languageService = Mock.Of<ILanguageService>();
            var cart = Mock.Of<Cart>();
            var productRepository = Mock.Of<ProductRepository>();
            var orderRepository = Mock.Of<IOrderRepository>();
            var localizer = Mock.Of<IStringLocalizer<ProductService>>();
            var productController = (new ProductController(new ProductService(cart, productRepository, orderRepository, localizer), languageService));
            var product = new ProductViewModel()

            {
                Name = string.Empty,

            };
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
            // Act
            var result = productController.Create(product) as ViewResult;
            var values = result.ViewData.ModelState.Values;
            List<string> expected = new List<string>();
            foreach (var res in values)
            {
                foreach (var err in res.Errors)
                {
                    expected.Add(err.ErrorMessage);
                    //Console.WriteLine(expected);
                }
            }
            // Assert
            Assert.Equal("Merci de saisir un nom de produit", expected.First());
        }


        [Fact]


        public void ShouldMissingPrice_Test()
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
            CultureInfo.CurrentUICulture = new CultureInfo("en-GB");
            // Act
            var result = productController.Create(product) as ViewResult;
            var modelStateValues = result.ViewData.ModelState.Values;
            List<string> expected = new List<string>();
            foreach (var res in modelStateValues)
            {
                foreach (var err in res.Errors)
                {
                    expected.Add(err.ErrorMessage);
                }
                // int indice = expected.IndexOf("The Price field is required");
            }

            //Assert
            Assert.Contains("The Price field is required", expected);
        }
        [Fact]

        public void ShouldIfPriceNotANumber_Test()
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
                Price = "p",
                // Stock = "2",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("en-GB");
            // Act
            var result = productController.Create(product) as ViewResult;
            var modelStateValues = result.ViewData.ModelState.Values;
            List<string> expected = new List<string>();
            foreach (var res in modelStateValues)
            {
                foreach (var err in res.Errors)
                {
                    expected.Add(err.ErrorMessage);
                }
            }
            //Assert
            Assert.Contains("The price must be a number Decima", expected);
        }
        [Fact]
        public void ShouldPrixSuperieura_0_Test()
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
                Price = "0",
                // Stock = "2",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
            // Act
            var result = productController.Create(product) as ViewResult;
            var modelStateValues = result.ViewData.ModelState.Values;
            List<string> expected = new List<string>();
            foreach (var res in modelStateValues)
            {
                foreach (var err in res.Errors)
                {
                    expected.Add(err.ErrorMessage);
                }
            }
            //Assert
            Assert.Contains("Le prix doit être supérieur à zéro", expected);
        }
        public void ShouldPriceNotGreaterThan0_Test()

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
                Price = "0",
                //  Stock = "2",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("en-GB");
            // Act
            var result = productController.Create(product) as ViewResult;
            var modelStateValues = result.ViewData.ModelState.Values;
            List<string> expected = new List<string>();
            foreach (var res in modelStateValues)
            {
                foreach (var err in res.Errors)
                {
                    expected.Add(err.ErrorMessage);
                }
            }
            //Assert
            Assert.Contains("The price must be greater than zero", expected);

        }
        [Fact]
        public void ShouldSaisirLaQuantite_Test()
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
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
            // Act
            var result = productController.Create(product) as ViewResult;
            var modelStateValues = result.ViewData.ModelState.Values;
            List<string> expected = new List<string>();
            foreach (var res in modelStateValues)
            {
                foreach (var err in res.Errors)
                {
                    expected.Add(err.ErrorMessage);
                }
            }
            //Assert
            Assert.Contains("Veuillez saisir une quantité", expected);

        }

        [Fact]
        public void ShouldIfTheQuantityNotAnInteger_Test()
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
            CultureInfo.CurrentUICulture = new CultureInfo("en-GB");
            // Act
            var result = productController.Create(product) as ViewResult;
            var modelStateValues = result.ViewData.ModelState.Values;
            List<string> expected = new List<string>();
            foreach (var res in modelStateValues)
            {
                foreach (var err in res.Errors)
                {
                    expected.Add(err.ErrorMessage);
                }
            }
            //Assert
            Assert.Contains("The quantity must be an integer", expected);
        }
        [Fact]

        public void ShouldQuantityIsGreaterThanZero_Test()
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
                //Price = "200",
                Stock = "-32",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("en-GB");
            // Act
            var result = productController.Create(product) as ViewResult;
            var modelStateValues = result.ViewData.ModelState.Values;
            List<string> expected = new List<string>();
            foreach (var res in modelStateValues)
            {
                foreach (var err in res.Errors)
                {
                    expected.Add(err.ErrorMessage);
                }
            }
            //Assert
            Assert.Contains("The quantity must be greater than zero", expected);
        }
        [Fact]

        public void ShouldFieldName_Test()
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
                Name = string.Empty,
                Price = "200",
                Stock = "2",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
            // Act
            var result = productController.Create(product) as ViewResult;
            var values = result.ViewData.ModelState.Values;
            List<string> expected = new List<string>();
            foreach (var res in values)
            {
                foreach (var err in res.Errors)
                {
                    expected.Add(err.ErrorMessage);
                    Console.WriteLine(expected);
                }
            }
            // Assert
            Assert.Contains("Veuillez saisir un nom", expected);
        }


        [Fact]
        public void ShouldSiPrixAbsent_Test()
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
                Price = "",
                // Stock = "2",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
            // Act
            var result = productController.Create(product) as ViewResult;
            var modelStateValues = result.ViewData.ModelState.Values;
            List<string> expected = new List<string>();
            foreach (var res in modelStateValues)
            {
                foreach (var err in res.Errors)
                {
                    expected.Add(err.ErrorMessage);
                }
            }
            //Assert
            Assert.Contains("Veuillez saisir un prix", expected);
        }

        [Fact]
        public void ShouldPrixSuperieurA_0_Test()
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
                Price = "0",
                //Stock = "2",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
            // Act
            var result = productController.Create(product) as ViewResult;
            var modelStateValues = result.ViewData.ModelState.Values;
            List<string> expected = new List<string>();
            foreach (var res in modelStateValues)
            {
                foreach (var err in res.Errors)
                {
                    expected.Add(err.ErrorMessage);
                }
            }
            //Assert
            Assert.Contains("Le prix doit être supérieur à zéro", expected);
        }



        // TODO write test methods to ensure a correct coverage of all possibilities


        /*
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
        }*/

        // TODO write test methods to ensure a correct coverage of all possibilities


    }
}

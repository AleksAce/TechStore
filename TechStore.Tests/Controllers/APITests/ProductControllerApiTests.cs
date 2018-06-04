using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess.Abstract;

using Models;
using Moq;
using NUnit.Framework;
using TechStore.Controllers;
using TechStore.Models.ViewModels;

namespace TechStore.Tests.Controllers.APITests
{
    [TestFixture]
    public class ProductControllerApiTests
    {
        Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
        List<Product> FakeProducts = new List<Product> { new Product() { ProductID = 1,Name = "prod1", isForMainPage = true},
                                                         new Product() { ProductID = 2,Name = "prod2"},
                                                         new Product() { ProductID = 1,Name = "prod3"},
                                                         new Product() { ProductID = 2,Name = "prod4"},
                                                         new Product() { ProductID = 1,Name = "prod5"},
                                                         new Product() { ProductID = 2,Name = "prod6"}};
        public ProductControllerApiTests()
        {
            _productRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(FakeProducts);
            _productRepository.Setup(r => r.GetByIDAsync(1)).ReturnsAsync(FakeProducts[0]);
            _productRepository.Setup(r => r.GetByIDAsync(2)).ReturnsAsync(FakeProducts[1]);
            _productRepository.Setup(r => r.GetByIDAsync(3)).ReturnsAsync(FakeProducts[2]);
            _productRepository.Setup(r => r.GetByIDAsync(4)).ReturnsAsync(FakeProducts[3]);
            _productRepository.Setup(r => r.GetByIDAsync(5)).ReturnsAsync(FakeProducts[4]);
            _productRepository.Setup(r => r.GetByIDAsync(6)).ReturnsAsync(FakeProducts[5]);
        }
        [TestCase(0,2)]
        [TestCase(1,2)]
        public void GetProductChunk_GivenProperIndexesAndChunkSize_ShouldReturnPropperNumberOfItems(int index, int numItems)
        {
            ProductController productController = new ProductController(_productRepository.Object);
            productController.Request = new HttpRequestMessage();
            productController.Configuration =new HttpConfiguration();


            HttpResponseMessage result = productController.Get(index, numItems).GetAwaiter().GetResult();
            var resultingProducts = new List<ProductsViewModel>();
            result.TryGetContentValue<List<ProductsViewModel>>(out resultingProducts);

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(2, resultingProducts.Count);
        }
        [TestCase(100, 2)]
        public void GetProductChunk_GivenBadIndex_ShouldReturn0Items(int index, int numItems)
        {
            ProductController productController = new ProductController(_productRepository.Object);
            productController.Request = new HttpRequestMessage();
            productController.Configuration = new HttpConfiguration();


            HttpResponseMessage result = productController.Get(index, numItems).GetAwaiter().GetResult();
            var resultingProducts = new List<ProductsViewModel>();
            result.TryGetContentValue<List<ProductsViewModel>>(out resultingProducts);

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(0, resultingProducts.Count);
        }
        [TestCase(0, 20)]
        public void GetProductChunk_GivenBiggerChunkSize_ShouldReturnAllItems(int index, int numItems)
        {
            ProductController productController = new ProductController(_productRepository.Object);
            productController.Request = new HttpRequestMessage();
            productController.Configuration = new HttpConfiguration();


            HttpResponseMessage result = productController.Get(index, numItems).GetAwaiter().GetResult();
            var resultingProducts = new List<ProductsViewModel>();
            result.TryGetContentValue<List<ProductsViewModel>>(out resultingProducts);

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(FakeProducts.Count, resultingProducts.Count);
        }
        [TestCase(true)]
        public void IsForMainPage_Given1MainPageFakeProduct_Returns1ProductOnly(bool isForMainpage)
        {
            _productRepository.Setup(r => r.GetMainPageProducts()).ReturnsAsync(new List<Product> { FakeProducts[0] });
            ProductController productController = new ProductController(_productRepository.Object);
            productController.Request = new HttpRequestMessage();
            productController.Configuration = new HttpConfiguration();


            HttpResponseMessage result = productController.Get(isForMainpage).GetAwaiter().GetResult();
            var resultingProducts = new List<ProductsViewModel>();
            result.TryGetContentValue<List<ProductsViewModel>>(out resultingProducts);

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(1, resultingProducts.Count);
        }
        [TestCase(true)]
        public void IsForMainPage_Given0MainPageFakeProduct_Returns0ProductOnly(bool isForMainpage)
        {
            _productRepository.Setup(r => r.GetMainPageProducts()).ReturnsAsync(new List<Product> ());
            ProductController productController = new ProductController(_productRepository.Object);
            productController.Request = new HttpRequestMessage();
            productController.Configuration = new HttpConfiguration();


            HttpResponseMessage result = productController.Get(isForMainpage).GetAwaiter().GetResult();
            var resultingProducts = new List<ProductsViewModel>();
            result.TryGetContentValue<List<ProductsViewModel>>(out resultingProducts);

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(0, resultingProducts.Count);
        }
        [TestCase(1)]
        [TestCase(2)]
        public void Get_GivenProperId_ReturnsPropperProduct(int Id)
        {
            ProductController productController = new ProductController(_productRepository.Object);
            productController.Request = new HttpRequestMessage();
            productController.Configuration = new HttpConfiguration();

            HttpResponseMessage result = productController.Get(Id).GetAwaiter().GetResult();
            var resultingProduct = new ProductsViewModel(new Product());
            result.TryGetContentValue<ProductsViewModel>(out resultingProduct);

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(Id, resultingProduct.ProductID);
        }
        [TestCase(0)]
        [TestCase(10000000)]
        public void Get_GivenBadID_ReturnsNotFound(int Id)
        {
            ProductController productController = new ProductController(_productRepository.Object);
            productController.Request = new HttpRequestMessage();
            productController.Configuration = new HttpConfiguration();

            HttpResponseMessage result = productController.Get(Id).GetAwaiter().GetResult();
            var resultingProduct = new ProductsViewModel(new Product());
            result.TryGetContentValue<ProductsViewModel>(out resultingProduct);

            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NotFound, result.StatusCode);
            Assert.AreEqual(null, resultingProduct);
        }
    }
}

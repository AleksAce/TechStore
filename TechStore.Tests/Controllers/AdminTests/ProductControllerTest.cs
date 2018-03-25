using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataAccess.Abstract;
using Models;
using Moq;
using NUnit.Framework;
using TechStore;
using TechStore.Controllers.Admin;
using TechStore.Models.ViewModels;

namespace TechStore.Tests.Controllers
{
    [TestFixture]
    public class ProductControllerTests
    {
         Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
         Mock<ICategoryRepository> _categoryRepository = new Mock<ICategoryRepository>();
        List<Product> FakeProducts = new List<Product> { new Product() { ProductID = 1,Name = "prod1"},
                                                         new Product() { ProductID = 2,Name = "prod2"} };

        List<Category> FakeCategories = new List<Category> { new Category() { CategoryID = 1, Name = "Cat1" },
                                                             new Category() { CategoryID = 2, Name = "Cat2" } };
        CreateProductViewModel TestCVM = new CreateProductViewModel()
        {
            Name = null,
            Description = "Test Product View Model"

        };
        public ProductControllerTests()
        {
                _productRepository.Setup(r => r.GetAll()).ReturnsAsync(FakeProducts);
            _productRepository.Setup(r => r.GetByIDAsync(1)).ReturnsAsync(FakeProducts[0]);
            _productRepository.Setup(r => r.GetByIDAsync(2)).ReturnsAsync(FakeProducts[1]);

            _categoryRepository.Setup(c => c.GetAll()).ReturnsAsync(FakeCategories);
            _categoryRepository.Setup(r => r.GetByIDAsync(1)).ReturnsAsync(FakeCategories[0]);
            _categoryRepository.Setup(r => r.GetByIDAsync(2)).ReturnsAsync(FakeCategories[1]);
        }
        [Test]
        public async Task Index_ReturnsExpectedResult()
        {               
            // Arrange
            ProductController controller = new ProductController(_productRepository.Object,_categoryRepository.Object);
        
            // Act
            ViewResult result = await controller.Index() as ViewResult;
            

            //Assert
            List<CreateProductViewModel> products = (List<CreateProductViewModel>)result.Model;
            Assert.NotNull(products);
            Assert.True(products.Count >= 2);
            Assert.NotNull(result);
            Assert.IsTrue("Index" == result.ViewName);

        }
        [TestCase(1)]
        [TestCase(2)]
        public async Task Details_ReturnsExpectedResult(int id)
        {
            // Arrange
            ProductController controller = new ProductController(_productRepository.Object, _categoryRepository.Object);

            // Act
            ViewResult result = await controller.Details(id) as ViewResult;

            //Assert
            CreateProductViewModel productVM = (CreateProductViewModel)result.Model;
            Assert.NotNull(result);
            Assert.NotNull(productVM);
            Assert.True(productVM.ProductID == id);
        }
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5000)]
        public async Task Details_GivenWrongID_ReturnsErrorPage(int id)
        {
            // Arrange
            ProductController controller = new ProductController(_productRepository.Object, _categoryRepository.Object);

            // Act
            ViewResult result = await controller.Details(id) as ViewResult;

            //Assert
            Assert.IsTrue(result.ViewBag.Message == "Product Not Found");
            Assert.IsTrue("Error" == result.ViewName);
        }
       
        [Test]
        public async Task Create_OnPost_WithModelStateInvalid_ReturnsCreateViewWithSameData()
        {
            // Arrange
            ProductController controller = new ProductController(_productRepository.Object, _categoryRepository.Object);
            controller.ModelState.AddModelError("Required", "Name Parameter Required");
            // Act
            ViewResult result = await controller.Create(TestCVM) as ViewResult;

            CreateProductViewModel resultCVM = (CreateProductViewModel)result.Model;
            //Assert
            Assert.IsTrue("Create" == result.ViewName);
            Assert.IsTrue(resultCVM.Description == TestCVM.Description);
        }
       
        
    }
}

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
    public class CategoryControllerTest
    {
        Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();
        Mock<ICategoryRepository> _categoryRepository = new Mock<ICategoryRepository>();
       
        List<Product> FakeProducts = new List<Product> { new Product() { ProductID = 1,Name = "prod1"},
                                                         new Product() { ProductID = 2,Name = "prod2"} };

        List<Category> FakeCategories = new List<Category> { new Category() { CategoryID = 1, Name = "Cat1" },
                                                             new Category() { CategoryID = 2, Name = "Cat2" } };
       
        CreateCategoryViewModel TestCVM = new CreateCategoryViewModel()
        {
            Name = null,
          
        };
        public CategoryControllerTest()
        {
            _productRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(FakeProducts);
            _productRepository.Setup(r => r.GetByIDAsync(1)).ReturnsAsync(FakeProducts[0]);
            _productRepository.Setup(r => r.GetByIDAsync(2)).ReturnsAsync(FakeProducts[1]);

            _categoryRepository.Setup(c => c.GetAllAsync()).ReturnsAsync(FakeCategories);
            _categoryRepository.Setup(r => r.GetByIDAsync(1)).ReturnsAsync(FakeCategories[0]);
            _categoryRepository.Setup(r => r.GetByIDAsync(2)).ReturnsAsync(FakeCategories[1]);
        }
        [Test]
        public async Task Index_ReturnsExpectedResult()
        {
            // Arrange
            CategoryController controller = new CategoryController( _categoryRepository.Object, _productRepository.Object);

            // Act
            ViewResult result = await controller.Index() as ViewResult;


            //Assert
            List<CreateCategoryViewModel> categories = (List<CreateCategoryViewModel>)result.Model;
            Assert.NotNull(categories);
            Assert.True(categories.Count >= 2);
            Assert.NotNull(result);
            Assert.IsTrue("Index" == result.ViewName);

        }
        [TestCase(1)]
        [TestCase(2)]
        public async Task Details_ReturnsExpectedResult(int id)
        {
            // Arrange
            CategoryController controller = new CategoryController(_categoryRepository.Object, _productRepository.Object);
            // Act
            ViewResult result = await controller.Details(id) as ViewResult;

            //Assert
            CreateCategoryViewModel categoryVM = (CreateCategoryViewModel)result.Model;
            Assert.NotNull(result);
            Assert.NotNull(categoryVM);
            Assert.True(categoryVM.CategoryID == id);
        }
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5000)]
        public async Task Details_GivenWrongID_ReturnsErrorPage(int id)
        {
            // Arrange
            CategoryController controller = new CategoryController(_categoryRepository.Object, _productRepository.Object);
            // Act
            ViewResult result = await controller.Details(id) as ViewResult;

            //Assert
            Assert.IsTrue("Error" == result.ViewName);
        }

        [Test]
        public async Task Create_OnPost_WithModelStateInvalid_ReturnsCreateViewWithSameData()
        {
            // Arrange
            // Arrange
            CategoryController controller = new CategoryController(_categoryRepository.Object, _productRepository.Object); controller.ModelState.AddModelError("Required", "Name Parameter Required");
            // Act
            ViewResult result = await controller.Create(TestCVM) as ViewResult;

            CreateCategoryViewModel resultCVM = (CreateCategoryViewModel)result.Model;
            //Assert
            Assert.IsTrue("Create" == result.ViewName);
        }
        [Test]
        public async Task Edit_OnPost_WithModelStateInvalid_ReturnsEditViewWithSameData()
        {
            // Arrange
            CategoryController controller = new CategoryController(_categoryRepository.Object, _productRepository.Object); controller.ModelState.AddModelError("Required", "Name Parameter Required");
            // Act
            ViewResult result = await controller.Edit(TestCVM) as ViewResult;

            CreateCategoryViewModel resultCVM = (CreateCategoryViewModel)result.Model;
            //Assert
            Assert.IsTrue("Edit" == result.ViewName);
        }
       
        [Test]
        public async Task ProductsPerCategory_Returns_ExpectedProducts()
        {
            // Arrange
            Product product1 = new Product() { ProductID = 1, Name = "prod1" };
            Product product2 = new Product() { ProductID = 2, Name = "prod2" };
            Product product3 = new Product() { ProductID = 3, Name = "prod3" };
            Category fakeCategory1 = new Category() { Products = new List<Product>() { product1, product2 } };
            List<Product> fakeProductList = new List<Product>() { product1, product2,product3 };
            _categoryRepository.Setup(p => p.GetByIDAsync(1)).ReturnsAsync(fakeCategory1);
            _productRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(fakeProductList);

            CategoryController controller = new CategoryController( _categoryRepository.Object, _productRepository.Object);
            // Act
            JsonResult result = await controller.ProductsPerCategory(1) as JsonResult;

            List<CategoryProductViewModel> resultCPVM = result.Data as List<CategoryProductViewModel>;
            //Assert
            Assert.IsTrue(resultCPVM.Count == 2);
            Assert.IsTrue(resultCPVM[0].ProductName == "prod1");
            Assert.IsTrue(resultCPVM[1].ProductName == "prod2");

        }
        [Test]
        public async Task ProductsNotInCategory_Returns_ExpectedProducts()
        {
            // Arrange
            //Setup their relationship
            Category fakeCategory1 = new Category() { CategoryID = 1, Products = new List<Product>() { } };
            Product product1 = new Product() { ProductID = 1, Name = "prod1" , Categories = new List<Category>() { fakeCategory1} };
            Product product2 = new Product() { ProductID = 2, Name = "prod2" , Categories = new List<Category>() { fakeCategory1 } };
            Product product3 = new Product() { ProductID = 3, Name = "prod3" , Categories = new List<Category>() { } };
            fakeCategory1 = new Category() { CategoryID = 1,Products = new List<Product>() { product1, product2 } };
            List<Product> fakeProductList = new List<Product>() { product1, product2, product3 };
            _categoryRepository.Setup(p => p.GetByIDAsync(1)).ReturnsAsync(fakeCategory1);
            _productRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(fakeProductList);

            CategoryController controller = new CategoryController(_categoryRepository.Object, _productRepository.Object);
            // Act
            JsonResult result = await controller.ProductsNotInCategory(1) as JsonResult;

            List<CategoryProductViewModel> resultCPVM = result.Data as List<CategoryProductViewModel>;
            //Assert
            Assert.IsTrue(resultCPVM.Count == 1);
            Assert.IsTrue(resultCPVM[0].ProductName == "prod3");

        }



    }
}

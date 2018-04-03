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
        Mock<IOrderRepository> _orderRepository = new Mock<IOrderRepository>();
        List<Product> FakeProducts = new List<Product> { new Product() { ProductID = 1,Name = "prod1"},
                                                         new Product() { ProductID = 2,Name = "prod2"} };

        List<Category> FakeCategories = new List<Category> { new Category() { CategoryID = 1, Name = "Cat1" },
                                                             new Category() { CategoryID = 2, Name = "Cat2" } };
        List<Order> FakeOrders = new List<Order> { new Order() { OrderID = 1, OrderDate= System.DateTime.Now },
                                                   new Order() { OrderID = 2, OrderDate= System.DateTime.Now } };
        CreateProductViewModel TestCVM = new CreateProductViewModel()
        {
            Name = null,
            Description = "Test Product View Model"

        };
        public ProductControllerTests()
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
            ProductController controller = new ProductController(_productRepository.Object,_categoryRepository.Object, _orderRepository.Object);
        
            // Act
            ViewResult result = await controller.Index() as ViewResult;
            

            //Assert
            List<CreateProductViewModel> products = (List<CreateProductViewModel>)result.Model;
            Assert.NotNull(products);
            Assert.True(products.Count >= 2);
            Assert.NotNull(result);
            Assert.IsTrue("Index" == result.ViewName);

        }
    
        [TestCase(2)]
        public async Task Details_ReturnsExpectedResult(int id)
        {
            // Arrange
            ProductController controller = new ProductController(_productRepository.Object, _categoryRepository.Object, _orderRepository.Object);

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
            ProductController controller = new ProductController(_productRepository.Object, _categoryRepository.Object, _orderRepository.Object);

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
            ProductController controller = new ProductController(_productRepository.Object, _categoryRepository.Object, _orderRepository.Object);
            controller.ModelState.AddModelError("Required", "Name Parameter Required");
            // Act
            ViewResult result = await controller.Create(TestCVM) as ViewResult;

            CreateProductViewModel resultCVM = (CreateProductViewModel)result.Model;
            //Assert
            Assert.IsTrue("Create" == result.ViewName);
            Assert.IsTrue(resultCVM.Description == TestCVM.Description);
        }
        [Test]
        public async Task Edit_OnPost_WithModelStateInvalid_ReturnsEditViewWithSameData()
        {
            // Arrange
            ProductController controller = new ProductController(_productRepository.Object, _categoryRepository.Object, _orderRepository.Object);
            controller.ModelState.AddModelError("Required", "Name Parameter Required");
            // Act
            ViewResult result = await controller.Edit(TestCVM) as ViewResult;

            CreateProductViewModel resultCVM = (CreateProductViewModel)result.Model;
            //Assert
            Assert.IsTrue("Edit" == result.ViewName);
            Assert.IsTrue(resultCVM.Description == TestCVM.Description);
        }
      
        [Test]
        public async Task CategoriesPerProduct_Returns_ExpectedProducts()
        {
            // Arrange
            Category category1 = new Category() { CategoryID = 1, Name = "cat1" };
            Category category2 = new Category() { CategoryID = 2, Name = "cat2" };
            Category category3 = new Category() { CategoryID = 3, Name = "cat3" };
            Product fakeProduct1 = new Product() { Categories = new List<Category>() { category1, category2 } };
            List<Category> fakeCategoryList = new List<Category>() { category1, category2, category3 };
            _productRepository.Setup(p => p.GetByIDAsync(1)).ReturnsAsync(fakeProduct1);
            _categoryRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(fakeCategoryList);

            ProductController controller = new ProductController(_productRepository.Object,_categoryRepository.Object,_orderRepository.Object);
            // Act
            JsonResult result = await controller.CategoriesPerProduct(1) as JsonResult;

            List<ProductCategoryViewModel> resultPCVM = result.Data as List<ProductCategoryViewModel>;
            //Assert
            Assert.IsTrue(resultPCVM.Count == 2);
            Assert.IsTrue(resultPCVM[0].CategoryName == "cat1");
            Assert.IsTrue(resultPCVM[1].CategoryName == "cat2");

        }
        [Test]
        public async Task ProductsNotInOrder_Returns_ExpectedProducts()
        {
            // Arrange
            //Setup their relationship
            Product fakeProduct1 = new Product() {ProductID = 1, Categories = new List<Category>() {  } };
            Category category1 = new Category() { CategoryID = 1, Name = "cat1", Products = new List<Product>() { fakeProduct1} };
            Category category2 = new Category() { CategoryID = 2, Name = "cat2", Products = new List<Product>() { fakeProduct1 } };
            Category category3 = new Category() { CategoryID = 3, Name = "cat3", Products = new List<Product>() {  } };
            fakeProduct1 = new Product() {ProductID = 1, Categories = new List<Category>() { category1, category2 } };
            List<Category> fakeCategoryList = new List<Category>() { category1, category2, category3 };

            _productRepository.Setup(p => p.GetByIDAsync(1)).ReturnsAsync(fakeProduct1);
            _categoryRepository.Setup(p => p.GetAllAsync()).ReturnsAsync(fakeCategoryList);
            ProductController controller = new ProductController(_productRepository.Object, _categoryRepository.Object, _orderRepository.Object);
            // Act
            JsonResult result = await controller.CategoriesNotInProduct(1) as JsonResult;

            List<ProductCategoryViewModel> resultPCVM = result.Data as List<ProductCategoryViewModel>;
            //Assert
            Assert.IsTrue(resultPCVM.Count == 1);
            Assert.IsTrue(resultPCVM[0].CategoryName == "cat3");

        }



    }
}

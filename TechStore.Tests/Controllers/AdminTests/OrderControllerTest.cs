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
    public class OrderControllerTests
    {
        Mock<IProductRepository> _productRepository = new Mock<IProductRepository>();

        Mock<ICustomerRepository> _customerRepository = new Mock<ICustomerRepository>();
        Mock<IOrderRepository> _orderRepository = new Mock<IOrderRepository>();
        List<Customer> FakeCustomers = new List<Customer> { new Customer() { CustomerID = 1,UserName = "cust1"},
                                                         new Customer() { CustomerID = 2,UserName = "cust2"} };

        List<Order> FakeOrders = new List<Order> { new Order() { OrderID = 1, OrderDate= System.DateTime.Now },
                                                   new Order() { OrderID = 2, OrderDate= System.DateTime.Now } };
        List<Product> FakeProducts = new List<Product> { new Product() { ProductID = 1,Name = "prod1"},
                                                         new Product() { ProductID = 2,Name = "prod2"} };

        CreateOrderViewModel TestCVM = new CreateOrderViewModel()
        {
            CustomerName = "Cust1"
        };
        public OrderControllerTests()
        {
            _customerRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(FakeCustomers);
            _customerRepository.Setup(r => r.GetByIDAsync(1)).ReturnsAsync(FakeCustomers[0]);
            _customerRepository.Setup(r => r.GetByIDAsync(2)).ReturnsAsync(FakeCustomers[1]);
            _customerRepository.Setup(r => r.GetCustomerByNameAsync("cust1")).ReturnsAsync(FakeCustomers[0]);
            _customerRepository.Setup(r => r.GetCustomerByNameAsync("cust2")).ReturnsAsync(FakeCustomers[1]);

            _productRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(FakeProducts);
            _productRepository.Setup(r => r.GetByIDAsync(1)).ReturnsAsync(FakeProducts[0]);
            _productRepository.Setup(r => r.GetByIDAsync(2)).ReturnsAsync(FakeProducts[1]);

            _orderRepository.Setup(o => o.GetAllAsync()).ReturnsAsync(FakeOrders);
            _orderRepository.Setup(o => o.GetByIDAsync(1)).ReturnsAsync(FakeOrders[0]);
            _orderRepository.Setup(o => o.GetByIDAsync(2)).ReturnsAsync(FakeOrders[1]);

        }
        [Test]
        public async Task Index_ReturnsExpectedResult()
        {
            // Arrange
            OrderController controller = new OrderController(_orderRepository.Object, _customerRepository.Object, _productRepository.Object);

            // Act
            ViewResult result = await controller.Index() as ViewResult;


            //Assert
            List<CreateOrderViewModel> orders = (List<CreateOrderViewModel>)result.Model;
            Assert.NotNull(orders);
            Assert.True(orders.Count >= 2);
            Assert.NotNull(result);
            Assert.IsTrue("Index" == result.ViewName);

        }
        [TestCase(1)]
        [TestCase(2)]
        public async Task Details_ReturnsExpectedResult(int id)
        {
            // Arrange
            OrderController controller = new OrderController(_orderRepository.Object, _customerRepository.Object, _productRepository.Object);

            // Act
            ViewResult result = await controller.Details(id) as ViewResult;

            //Assert
            CreateOrderViewModel orderVM = (CreateOrderViewModel)result.Model;
            Assert.NotNull(result);
            Assert.NotNull(orderVM);
            Assert.True(orderVM.OrderID == id);
        }
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5000)]
        public async Task Details_GivenWrongID_ReturnsErrorPage(int id)
        {
            // Arrange
            OrderController controller = new OrderController(_orderRepository.Object, _customerRepository.Object, _productRepository.Object);

            // Act
            ViewResult result = await controller.Details(id) as ViewResult;

            //Assert
            Assert.IsTrue("Error" == result.ViewName);
        }

        [Test]
        public async Task Create_OnPost_WithNonExistingCustomer_ReturnsErrorMessageWithSameView()
        {
            // Arrange
            OrderController controller = new OrderController(_orderRepository.Object, _customerRepository.Object, _productRepository.Object);

            // Act
            CreateOrderViewModel testOVM2WithNonexistingCustomer = new CreateOrderViewModel() { CustomerName = "NoCustomer" };

            ViewResult result = await controller.Create(testOVM2WithNonexistingCustomer) as ViewResult;

            CreateOrderViewModel resultCVM = (CreateOrderViewModel)result.Model;
            //Assert
            Assert.IsTrue(result.ViewBag.Error != null);
            Assert.IsTrue("Create" == result.ViewName);
        }
        [Test]
        public async Task Edit_OnPost_WithNonExistingCustomer_ReturnsErrorMessageWithSameView()
        {
            // Arrange
            OrderController controller = new OrderController(_orderRepository.Object, _customerRepository.Object, _productRepository.Object);

            // Act
            CreateOrderViewModel testOVM2WithNonexistingCustomer = new CreateOrderViewModel() { CustomerName = "NoCustomer" };

            ViewResult result = await controller.Edit(testOVM2WithNonexistingCustomer) as ViewResult;

            CreateOrderViewModel resultCVM = (CreateOrderViewModel)result.Model;
            //Assert
            Assert.IsTrue(result.ViewBag.Error != null);
            Assert.IsTrue("Edit" == result.ViewName);
        }

        

       
       



    }
}


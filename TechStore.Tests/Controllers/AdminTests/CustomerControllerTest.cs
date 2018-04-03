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
    public class CustomerControllerTests
    {
        Mock<ICustomerRepository> _customerRepository = new Mock<ICustomerRepository>(); 
        Mock<IOrderRepository> _orderRepository = new Mock<IOrderRepository>();
        List<Customer> FakeCustomers = new List<Customer> { new Customer() { CustomerID = 1,UserName = "cust1"},
                                                         new Customer() { CustomerID = 2,UserName = "cust2"} };

        List<Order> FakeOrders = new List<Order> { new Order() { OrderID = 1, OrderDate= System.DateTime.Now },
                                                   new Order() { OrderID = 2, OrderDate= System.DateTime.Now } };
        CustomerViewModel TestCVM = new CustomerViewModel()
        { 
            UserName = null,
        };
        public CustomerControllerTests()
        {
            _customerRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(FakeCustomers);
            _customerRepository.Setup(r => r.GetByIDAsync(1)).ReturnsAsync(FakeCustomers[0]);
            _customerRepository.Setup(r => r.GetByIDAsync(2)).ReturnsAsync(FakeCustomers[1]);
            _customerRepository.Setup(r => r.GetCustomerByNameAsync("cust1")).ReturnsAsync(FakeCustomers[0]);
            _customerRepository.Setup(r => r.GetCustomerByNameAsync("cust2")).ReturnsAsync(FakeCustomers[1]);

        }
        [Test]
        public async Task Index_ReturnsExpectedResult()
        {
            // Arrange
            CustomerController controller = new CustomerController(_orderRepository.Object, _customerRepository.Object);

            // Act
            ViewResult result = await controller.Index() as ViewResult;


            //Assert
            List<CustomerViewModel> customers = (List<CustomerViewModel>)result.Model;
            Assert.NotNull(customers);
            Assert.True(customers.Count >= 2);
            Assert.NotNull(result);
            Assert.IsTrue("Index" == result.ViewName);

        }
        [TestCase(1)]
        [TestCase(2)]
        public async Task Details_ReturnsExpectedResult(int id)
        {
            // Arrange
            CustomerController controller = new CustomerController(_orderRepository.Object, _customerRepository.Object);

            // Act
            ViewResult result = await controller.Details(id) as ViewResult;

            //Assert
            CustomerViewModel customerVM = (CustomerViewModel)result.Model;
            Assert.NotNull(result);
            Assert.NotNull(customerVM);
            Assert.True(customerVM.CustomerID == id);
        }
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5000)]
        public async Task Details_GivenWrongID_ReturnsErrorPage(int id)
        {
            // Arrange
            CustomerController controller = new CustomerController(_orderRepository.Object, _customerRepository.Object);

            // Act
            ViewResult result = await controller.Details(id) as ViewResult;

            //Assert
            Assert.IsTrue("Error" == result.ViewName);
        }

        [Test]
        public async Task Create_OnPost_WithModelStateInvalid_ReturnsCreateViewWithSameData()
        {
            // Arrange
            CustomerController controller = new CustomerController(_orderRepository.Object, _customerRepository.Object);
            controller.ModelState.AddModelError("Required", "UserName Parameter Required");
            // Act
            ViewResult result = await controller.Create(TestCVM) as ViewResult;

            CustomerViewModel resultCVM = (CustomerViewModel)result.Model;
            //Assert
            Assert.IsTrue("Create" == result.ViewName);
        }
        [Test]
        public async Task Create_OnPost_WithSameUserName_ReturnsErrorMessageWithSameView()
        {
            // Arrange
            CustomerController controller = new CustomerController(_orderRepository.Object, _customerRepository.Object);
            
            // Act
            CustomerViewModel testCVM2WithSameName =new CustomerViewModel() { UserName = "cust1" }; 
            ViewResult result = await controller.Create(testCVM2WithSameName) as ViewResult;

            CustomerViewModel resultCVM = (CustomerViewModel)result.Model;
            //Assert
            Assert.IsTrue(result.ViewBag.Error != null);
            Assert.IsTrue("Create" == result.ViewName);
        }
        [Test]
        public async Task Edit_OnPost_WithModelStateInvalid_ReturnsEditViewWithSameData()
        {
            // Arrange
            CustomerController controller = new CustomerController(_orderRepository.Object, _customerRepository.Object);
            controller.ModelState.AddModelError("Required", "UserName Parameter Required");
            // Act
            ViewResult result = await controller.Edit(TestCVM) as ViewResult;

            CustomerViewModel resultCVM = (CustomerViewModel)result.Model;
            //Assert
            Assert.IsTrue("Edit" == result.ViewName);
        }
        [Test]
        public async Task Edit_OnPost_WithSameUserName_ReturnsEditViewWithERROR()
        {
            // Arrange
            CustomerController controller = new CustomerController(_orderRepository.Object, _customerRepository.Object);
            
            // Act
            CustomerViewModel testCVM2WithSameName = new CustomerViewModel() { UserName = "cust1" };
            ViewResult result = await controller.Edit(testCVM2WithSameName) as ViewResult;

            CustomerViewModel resultCVM = (CustomerViewModel)result.Model;
            //Assert
            Assert.IsTrue(result.ViewBag.Error != null);
            Assert.IsTrue("Edit" == result.ViewName);
        }
      



    }
}

using DataAccess.Abstract;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TechStore.Models.ViewModels;

namespace TechStore.Controllers.Admin
{
    public class CustomerController : Controller
    {
        private IOrderRepository _orderRepository;
        private ICustomerRepository _customerRepository;
     
        public CustomerController(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }
        [HttpGet]
        public async Task<string> RemoveOrderFromCustomer(int customerID, int orderID)
        {
            try
            {
                await _orderRepository.RemoveCustomerFromOrderAsync(orderID, customerID);
                await _orderRepository.SaveAll();
                return "Order successfully removed";
            }
            catch
            {
                return "Order failed to be removed";
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetOrders(int customerID)
        {
            Customer customer  = await _customerRepository.GetByIDAsync(customerID);
            List<CustomerOrderViewModel> covm = new List<CustomerOrderViewModel>();
            foreach (var o in customer.OrdersIssued)
            {
                CustomerOrderViewModel cvm = new CustomerOrderViewModel(o);
                covm.Add(cvm);
            }
            return Json(covm, JsonRequestBehavior.AllowGet);
        }
        // GET: Customer
        public async Task<ActionResult> Index()
        {
            List<CustomerViewModel> lcvm = new List<CustomerViewModel>();
            List<Customer> customers = await _customerRepository.GetAllAsync();
            foreach (var c in customers)
            {
                CustomerViewModel cvm = new CustomerViewModel(c);
                lcvm.Add(cvm);
            }
            return View("Index",lcvm);
           
        }

        // GET: Customer/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Customer customer = await _customerRepository.GetByIDAsync(id);
            if(customer == null)
            {
                return View("Error");
            }
            CustomerViewModel cvm = new CustomerViewModel(customer);
            return View("Details",cvm);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View(new CustomerViewModel());
        }

        // POST: Customer/Create
        [HttpPost]
        public async Task<ActionResult> Create(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            { 
                try
                {
                    
                    if ((await _customerRepository.GetCustomerByNameAsync(model.UserName)) != null)
                    {
                        ViewBag.Error = "Username Already Exists, please try using another one";
                        return View("Create",model);
                    }
                    Customer customer = new Customer()
                    {
                        UserName = model.UserName,
                        DateRegistered = DateTime.Now

                    };
                    _customerRepository.Add(customer);
                    await _customerRepository.SaveAll();
                   
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View("Create",model);
                }
            }
            else
            {
                return View("Create",model);
            }
        }

        // GET: Customer/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Customer customer = await _customerRepository.GetByIDAsync(id);
            CustomerViewModel cvm = new CustomerViewModel(customer);
            return View(cvm);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if ((await _customerRepository.GetCustomerByNameAsync(model.UserName)) != null)
                    {
                        ViewBag.Error = "Username Already Exists, please try using another one";
                        return View("Edit",model);
                    }
                    Customer customer = await _customerRepository.GetByIDAsync(model.CustomerID);
                    customer.CustomerID = model.CustomerID;
                    customer.UserName = model.UserName;
                    customer.DateRegistered = model.DateRegistered;

                    await _customerRepository.SaveAll();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View("Edit",model);
                }
            }
            else
            {
                return View("Edit",model);
            }
        }
        [HttpGet]
        public async Task<ActionResult> AddOrder(string userName)
        {
            Customer cust = await _customerRepository.GetCustomerByNameAsync(userName);

            await _orderRepository.CreateOrder(new List<Product>(), cust.CustomerID);
            await _orderRepository.SaveAll();
            return RedirectToAction("Edit", "Order", new { id= cust.OrdersIssued.Last().OrderID});
        }
        // GET: Customer/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Customer customer = await _customerRepository.GetByIDAsync(id);
            return View(new CustomerViewModel(customer));
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Customer customer = await _customerRepository.GetByIDAsync(id);
                _customerRepository.Delete(customer);
                await _customerRepository.SaveAll();
                return View("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

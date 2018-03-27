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
        // GET: Customer
        public async Task<ActionResult> Index()
        {
            List<CustomerViewModel> lcvm = new List<CustomerViewModel>();
            List<Customer> customers = await _customerRepository.GetAll();
            foreach (var c in customers)
            {
                CustomerViewModel cvm = new CustomerViewModel(c);
                lcvm.Add(cvm);
            }
            return View(lcvm);
           
        }

        // GET: Customer/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Customer customer = await _customerRepository.GetByIDAsync(id);
            CustomerViewModel cvm = new CustomerViewModel(customer);
            return View(cvm);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

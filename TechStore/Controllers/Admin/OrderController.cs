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
    public class OrderController : Controller
    {
        private IOrderRepository _orderRepository;
        private ICustomerRepository _customerRepository;

        public OrderController(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
        }
        
        // GET: Order

        public async Task<ActionResult> Index()
        {
            List<OrderViewModel> lovm = new List<OrderViewModel>();
            List<Order> orders = await _orderRepository.GetAll();
            foreach(var o in orders)
            {
                OrderViewModel ovm = new OrderViewModel(o);
                lovm.Add(ovm);
            }
            return View(lovm);
        }

        // GET: Order/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Order order = await _orderRepository.GetByIDAsync(id);
            OrderViewModel ovm = new OrderViewModel(order);
            return View(ovm);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Order/Create
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

        // GET: Order/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Order order = await _orderRepository.GetByIDAsync(id);
            OrderViewModel ovm = new OrderViewModel(order);
            return View(ovm);
        }

        // POST: Order/Edit/5
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

        // GET: Order/Delete/5
        public async Task<ActionResult>Delete(int id)
        {
            Order order = await _orderRepository.GetByIDAsync(id);
            OrderViewModel ovm = new OrderViewModel(order);
            return View(ovm);
        }

        // POST: Order/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Order order = await _orderRepository.GetByIDAsync(id);
                _orderRepository.Delete(order);
                await _orderRepository.SaveAll();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

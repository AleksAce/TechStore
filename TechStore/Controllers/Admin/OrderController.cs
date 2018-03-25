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

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
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
        public ActionResult Edit(int id)
        {
            return View();
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Order/Delete/5
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

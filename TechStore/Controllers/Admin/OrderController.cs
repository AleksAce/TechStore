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
        private IProductRepository _productRepository;

        public OrderController(IOrderRepository orderRepository, ICustomerRepository customerRepository,
            IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _customerRepository = customerRepository;
            _productRepository = productRepository;
        }
       
        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            List<Product> allProducts = await _productRepository.GetAllAsync();
            List<OrderProductViewModel> opvm = new List<OrderProductViewModel>();
            foreach (var c in allProducts)
            {
                OrderProductViewModel pvm = new OrderProductViewModel() {
                    ProductID = c.ProductID,
                    Price = c.Price,
                    ProductName = c.Name,
                };
                opvm.Add(pvm);
            }
            return Json(opvm, JsonRequestBehavior.AllowGet);
        }
        //Note: This gets the already payed prices
        [HttpGet]
        public async Task<ActionResult> ProductsPerOrder(int orderID)
        {
            Order order = await _orderRepository.GetByIDAsync(orderID);
            List<OrderProductViewModel> opvm = new List<OrderProductViewModel>();
            foreach (var c in order.ProductsOrderInfo)
            {
                var product = await _productRepository.GetByIDAsync(c.productID);
                OrderProductViewModel pvm = new OrderProductViewModel()
                {
                    ProductInfoID = c.ProductWithCompletedOrderID,
                    ProductID = c.productID,
                    Price = c.PricePayed,
                    ProductName = product.Name
                };
                opvm.Add(pvm);
            }
            return Json(opvm, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public async Task<string> RemoveProductFromOrder(int ProductInfoID, int orderID)
        {
            try
            {
                await _orderRepository.RemoveProductFromOrder(ProductInfoID, orderID);
                await _orderRepository.SaveAll();
                return "Product successfully removed";
            }
            catch
            {
                return "Product failed to be removed";
            }
        }
        [HttpGet]
        public async Task<string> AddProductToOrder(int productID, int orderID)
        {
            //Todo: FInd out why it refreshes
            try
            {
                await _orderRepository.AddProductToOrder(productID, orderID);
                await _orderRepository.SaveAll();
                return "Product successfully added";
            }
            catch
            {
                return "Product failed to be added";
            }
        }
        // GET: Order
       
        public async Task<ActionResult> Index()
        {
            List<CreateOrderViewModel> lovm = new List<CreateOrderViewModel>();
            List<Order> orders = await _orderRepository.GetAllAsync();
            foreach(var o in orders)
            {
                CreateOrderViewModel ovm = new CreateOrderViewModel(o);
                lovm.Add(ovm);
            }
            return View("Index",lovm);
        }

        // GET: Order/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Order order = await _orderRepository.GetByIDAsync(id);
            if(order == null)
            {
                return View("Error");
            }
            CreateOrderViewModel ovm = new CreateOrderViewModel(order);
            return View("Details",ovm);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            return View("Create", new CreateOrderViewModel());
        }

        // POST: Order/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateOrderViewModel model)
        {
            if (ModelState.IsValid)
            {

                try
                {
                   Customer customer = await _customerRepository.GetCustomerByNameAsync(model.CustomerName);
                    if(customer == null)
                    {
                        //Ensure there must be a customer
                        ViewBag.Error = "Customer does not Exist, Please Register the customer";
                        return View("Create",model);
                    }

                    Order order = new Order()
                    {
                        OrderDate = DateTime.Now,
                    };
                    _orderRepository.Add(order);
                    await _orderRepository.SaveAll();
                    await _orderRepository.AddCustomerToOrderAsync(order.OrderID, customer.CustomerID);
                    await _orderRepository.SaveAll();
                    // TODO: Add insert logic here
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

        // GET: Order/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Order order = await _orderRepository.GetByIDAsync(id);
            CreateOrderViewModel ovm = new CreateOrderViewModel(order);
            return View(ovm);
        }

        // POST: Order/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(CreateOrderViewModel model)
        {
            try
            {
               
                Customer customer = await _customerRepository.GetCustomerByNameAsync(model.CustomerName);
                if (customer == null)
                {
                    ViewBag.Error = "Customer does not Exist, Please register the customer";
                    //Ensure there must be a customer
                    return View("Edit",model);
                }
                Order order =await  _orderRepository.GetByIDAsync(model.OrderID);
                //order.
                await _orderRepository.AddCustomerToOrderAsync(model.OrderID, customer.CustomerID);
                await _orderRepository.SaveAll();
                // TODO: Add insert logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Edit",model);
            }
        }

        // GET: Order/Delete/5
        public async Task<ActionResult>Delete(int id)
        {
            Order order = await _orderRepository.GetByIDAsync(id);
            CreateOrderViewModel ovm = new CreateOrderViewModel(order);
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
                return View("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

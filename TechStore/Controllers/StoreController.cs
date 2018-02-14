using DataAccess.Abstract;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace TechStore.Controllers
{
    public class StoreController : Controller
    {
        ProductRepository Productrepo = new ProductRepository();
        OrderRepository Orderrepo = new OrderRepository();
        CategoryRepository CategoryRepo = new CategoryRepository();
      
        public async Task<ActionResult> Index()
        {
           
            List<Product> products =await Productrepo.GetAllAsync();
         //   List<Order> orders =await  Orderrepo.GetAllAsync();
         //   List<Category> categories =await CategoryRepo.GetAllAsync();

            return View();
        }
        public ActionResult Products()
        {
            return View();
        }
    }
}

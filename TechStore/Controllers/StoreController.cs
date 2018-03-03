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
   
      
        public async Task<ActionResult> Index()
        {
           
          
       

            return View();
        }
        public ActionResult Products()
        {
            return View();
        }
    }
}

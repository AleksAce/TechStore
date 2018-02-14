using DataAccess.Abstract;
using DataAccess.Services;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace TechStore.Controllers
{
    public class ProductController : ApiController
    {
        //Specific
        ProductService productService;
        //Global

        IStoreService<Order> orderService;
        public ProductController(ProductService prodService, IStoreService<Order> orderService)
        {
            productService = prodService;
            this.orderService = orderService;
        }
 
        [HttpGet]
        public async Task<HttpResponseMessage> Get()
        {
            List<Product> products = await productService.GetAllItemsAsync();
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int id)
        {
            Product product = await productService.GetItemByIDAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK, product);
        }
    }
}

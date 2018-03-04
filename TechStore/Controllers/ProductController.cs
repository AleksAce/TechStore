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
      
        ProductService productService;
        CategoryService _categoryService;
        public ProductController(ProductService prodService,
                                CategoryService categoryService)
        {
            productService = prodService;
            
            _categoryService = categoryService;
        }
 
        [HttpGet]
        public async Task<HttpResponseMessage> Get()
        {
            List<Product> products = await productService.GetAllItemsAsync();
            List<Category> categories = await _categoryService.GetAllItemsAsync();
            
            int[] num = { 1, 2, 3 };
            
            
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

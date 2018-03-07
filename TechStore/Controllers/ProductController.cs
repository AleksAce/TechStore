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
      
        IProductService _productService;
        ICategoryService _categoryService;
        
        public ProductController(IProductService prodService,
                                ICategoryService categoryService
                                )
        {
            _productService = prodService;
            _categoryService = categoryService;
 
        }
 
        [HttpGet]
        public async Task<HttpResponseMessage> Get()
        {
            
            
            List<Product> products = await _productService.GetAllItemsAsync();
            List<Category> categories = await _categoryService.GetAllItemsAsync();

            await _productService.AddToCategory(products[1].ProductID, categories[2].CategoryID);
            int[] num = { 1, 2, 3 };
            
            
            return Request.CreateResponse(HttpStatusCode.OK, products);
        }
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int id)
        {
            Product product = await _productService.GetItemByIDAsync(id);
            return Request.CreateResponse(HttpStatusCode.OK, product);
        }
    }
}

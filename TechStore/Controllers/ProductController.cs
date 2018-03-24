using DataAccess.Abstract;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TechStore.Models.ViewModels;

namespace TechStore.Controllers
{
    public class ProductController : ApiController
    {
        ProductRepository _productRepository;

        public ProductController()
        {
            _productRepository = new ProductRepository();

        }
        //TODO: SEE HOW TO USE THE SAME CONTEXT IN THE SERVICE SO YOU CAN USE IT TO ADD AND DELETE ITEMS FROM IT
        [HttpGet]
        public async Task<HttpResponseMessage> Get()
        {

            try
            {
            
            List<Product> products = await _productRepository.GetAllAsync();

            List<ProductsViewModel> productsViewModelList = new List<ProductsViewModel>();
            foreach (var p in products)
            {
                    ProductsViewModel pvm = new ProductsViewModel(p);
                    productsViewModelList.Add(pvm);
            }
                
               return  Request.CreateResponse(HttpStatusCode.OK, productsViewModelList);
            }
            catch(Exception ex)
            {
               return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Could not fetch products");
            }
            
        }
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int id)
        {
            try
            {
                Product product = await _productRepository.GetByIDAsync(id);
                
                return Request.CreateResponse(HttpStatusCode.OK, new ProductsViewModel(product));
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Could not fetch product with ID");
            }
        }
    }
}

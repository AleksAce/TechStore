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
        IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;

        }
        [HttpGet]
        [Route("api/Product/ProductChunk")]
        public async Task<HttpResponseMessage> Get(int index, int numItems)
        {
            try
            {

                List<Product> prods = await _productRepository.GetAllAsync();
                List<Product> products = prods.Skip(index*numItems).Take(numItems).ToList();
                List<ProductsViewModel> productsViewModelList = new List<ProductsViewModel>();
                foreach (var p in products)
                {
                    ProductsViewModel pvm = new ProductsViewModel(p);
                    productsViewModelList.Add(pvm);
                }

                return Request.CreateResponse(HttpStatusCode.OK, productsViewModelList);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Could not fetch products");
            }

        }
        [HttpGet]
        [Route("api/Product/GetForMainpage")]
        public async Task<HttpResponseMessage> Get(bool forMainPage = false)
        {
            try
            {

                List<Product> products = await _productRepository.GetMainPageProducts();

                List<ProductsViewModel> productsViewModelList = new List<ProductsViewModel>();
                foreach (var p in products)
                {
                    ProductsViewModel pvm = new ProductsViewModel(p);
                    productsViewModelList.Add(pvm);
                }

                return Request.CreateResponse(HttpStatusCode.OK, productsViewModelList);
            }
            catch (Exception ex)
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
                if(product == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                return Request.CreateResponse(HttpStatusCode.OK, new ProductsViewModel(product));
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.ExpectationFailed, "Could not fetch product with ID");
            }
        }
    }
}

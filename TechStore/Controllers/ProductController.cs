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
using TechStore.Models.ViewModels;

namespace TechStore.Controllers
{
    public class ProductController : ApiController
    {


        ProductRepository _productRepository;
        OrderRepository _orderRepository;
        CategoryRepository _categoryRepository;
        public ProductController()
        {
            _productRepository = new ProductRepository();
            _orderRepository = new OrderRepository();
            _categoryRepository = new CategoryRepository();

        }

        //TODO: SEE HOW TO USE THE SAME CONTEXT IN THE SERVICE SO YOU CAN USE IT TO ADD AND DELETE ITEMS FROM IT
        [HttpGet]
        public async Task<HttpResponseMessage> Get()
        {

            try
            {
                Product ProductToUse1 = _productRepository.GetProductByName("Product3");
                Product ProductToUse2 = _productRepository.GetProductByName("Product4");
                List<Product> productsList = new List<Product>();
                productsList.Add(ProductToUse1);
                
                await _orderRepository.CreateOrder(productsList);
                await _orderRepository.SaveAll();
                
                //Finally this works ONE CONTEXT FOR THE WIN
                await _orderRepository.AddProductToOrder(ProductToUse2.ProductID, 2);
                await _orderRepository.SaveAll();
                await _orderRepository.RemoveProductFromOrder(ProductToUse2.ProductID, 2);
                await _orderRepository.RemoveProductFromOrder(ProductToUse1.ProductID, 2);
                await _orderRepository.SaveAll();

            //BUG: Categories keep increasing
            List<Category> categories = await _categoryRepository.GetAllAsync();
            List<Order> orders = await _orderRepository.GetAllAsync();
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

using DataAccess.Abstract;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    //Service for products
    public interface IProductService
    {
        Product GetProductByName(string name);
    }
    //Specific Service for products
    public class ProductService : StoreBaseService<Product>, IProductService
    {
        //Specific for product while using the generic stuff
        protected IProductRepository productRepository;
        public ProductService() : base()
        {
            //Specific repository for product
            productRepository = new ProductRepository();
        }
        public Product GetProductByName(string name)
        {
            return productRepository.GetProductByName(name);
        }

    }
}

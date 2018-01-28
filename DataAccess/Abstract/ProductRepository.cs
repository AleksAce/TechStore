using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    //Specific to Products
    public interface IProductRepository
    {
        Product GetProductByName(string name);
    }
    public class ProductRepository : StoreBaseRepository<Product>  , IProductRepository
    {
        public ProductRepository() : base()
        {
            
        }

        public Product GetProductByName(string name)
        {
            return context.Products.Where(p => p.Name == name).SingleOrDefault();
        }
    }
}

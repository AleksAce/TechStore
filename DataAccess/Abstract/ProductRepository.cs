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
        Task AddToCategory(int productID, int categoryID);
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

        public async Task AddToCategory(int productID, int categoryID)
        {
            Product prod = await context.Products.FindAsync(productID);

            Category cat =await context.Categories.FindAsync(categoryID);
            cat.Products.Add(prod);
            await context.SaveChangesAsync();
        }

        
    }
}

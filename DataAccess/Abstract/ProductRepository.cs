using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        Task RemoveFromCategory(int productID, int categoryID);
    }
    //Important: USE THIS in your service
    public class ProductRepository : StoreBaseRepository<Product>  , IProductRepository
    {
        public ProductRepository() : base()
        {
            
        }

        public Product GetProductByName(string name)
        {
            return dbSet.Where(p => p.Name == name).SingleOrDefault();
            
        }
        public override async Task<List<Product>> GetAllAsync()
        {
            List<Product> products = await dbSet.Include(p => p.Category).ToListAsync();
            return products;
        }
        public async Task AddToCategory(int productID, int categoryID)
        {
            Product prod = await context.Products.FindAsync(productID);

            Category cat =await context.Categories.FindAsync(categoryID);
            prod.Category = cat;
            await context.SaveChangesAsync();
        }

        public async Task RemoveFromCategory(int productID, int categoryID)
        {
            Product prod = await context.Products.FindAsync(productID);
            Category cat = await context.Categories.FindAsync(categoryID);
            cat.Products.Remove(prod);
            await context.SaveChangesAsync();
        }
    }
}

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
    public interface IProductRepository : IStoreRepository<Product> 
    {
        Task<Product> GetProductByNameAsync(string name);
        Task AddProductToCategoryAsync(int productID, int categoryID);
        Task RemoveProductFromCategoryAsync(int productID, int categoryID);
        Task AddProductToCategoryAsync(string productName, string categoryName);
    }
    //StoreBase Repository Implements the IStoreRepository Interface, so no worries
    public class ProductRepository : StoreBaseRepository<Product>  , IProductRepository
    {
        public ProductRepository() : base()
        {
            
        }
        
        public async Task<Product> GetProductByNameAsync(string name)
        {
                return await dbSet.Include(p => p.Orders).Include(p => p.Category).SingleOrDefaultAsync(p => p.Name == name);
        }
        public override async Task<List<Product>> GetAll()
        {
            List<Product> products = dbSet.Include(p => p.Category).Include(p=>p.Orders).ToList();
            return products;
            
        }
        public async override Task<Product> GetByIDAsync(int id)
        {
                return await dbSet.Include(p => p.Category).Include(p => p.Orders).SingleOrDefaultAsync(p => p.ProductID == id);
        }

        public async Task AddProductToCategoryAsync(int productID, int categoryID)
        {
            try
            {
                Product prod = await GetByIDAsync(productID);
                Category cat = await context.Categories.FindAsync(categoryID);
                prod.Category = cat;
                //cat.Products.Add(prod);
            }catch(Exception ex)
            {
                //Could not Find Product or Category
                return;
            }
        }

        public async Task RemoveProductFromCategoryAsync(int productID, int categoryID)
        {
            try
            {
                Product prod = await GetByIDAsync(productID);
                Category cat = await context.Categories.FindAsync(categoryID);
                if (prod.Category == cat)
                {
                    prod.Category = null;
                }
                
            }catch(Exception ex)
            {
                //Could not find product or category
                return;
            }
        }

        public async Task AddProductToCategoryAsync(string productName, string categoryName)
        {
            try
            {
                Product prod = await GetProductByNameAsync(productName);
                Category cat = await context.Categories.SingleOrDefaultAsync(c=>c.Name == categoryName);
                prod.Category = cat;
                //cat.Products.Add(prod);
            }
            catch (Exception ex)
            {
                //Could not Find Product or Category
                return;
            }
        }
    }
}

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
        Task<Product> GetProductByName(string name);
        Task AddProductToCategoryAsync(int productID, int categoryID);
        Task RemoveProductFromCategoryAsync(int productID, int categoryID);

    }
    //Important: USE THIS in your service
    public class ProductRepository : StoreBaseRepository<Product>  , IProductRepository
    {
        public ProductRepository() : base()
        {
            
        }

        public async Task<Product> GetProductByName(string name)
        {
                return await dbSet.Include(p => p.Orders).Include(p => p.Category).SingleOrDefaultAsync(p => p.Name == name);
        }
        public override async Task<List<Product>> GetAllAsync()
        {
            List<Product> products = await dbSet.Include(p => p.Category).Include(p=>p.Orders).ToListAsync();
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
    }
}

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
        public async override Task<Product> GetByIDAsync(int id)
        {
            return await dbSet.Include(p=>p.Category).SingleOrDefaultAsync(p=>p.ProductID == id);
        }

    }
}

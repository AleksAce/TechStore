using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public class CategoryRepository : StoreBaseRepository<Category>, ICategoryRepository
    {

        public CategoryRepository() : base()
        {

        }
        public override async Task<List<Category>> GetAllAsync()
        {
            
            List<Category> categories = await dbSet.Include(x => x.Products).ToListAsync();
            return categories;
        }
        public override async Task<Category> GetByIDAsync(int id)
        {
            return await dbSet.Include(x => x.Products).SingleOrDefaultAsync(c=>c.CategoryID == id);
        }
      
        
    }
    //Specific to categories
    public interface ICategoryRepository
    {
    }
}

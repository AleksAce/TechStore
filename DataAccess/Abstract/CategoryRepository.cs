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
    }
    //Specific to categories
    internal interface ICategoryRepository
    {
    }
}

using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;

namespace DataAccess.Repositories
{
    public class CategoryRepository : StoreBaseRepository<Category>, ICategoryRepository
    {

        public CategoryRepository() : base()
        {

        }
        public override Task<List<Category>> GetAllAsync()
        {
            List<Category> categories = new List<Category>();

            try
            {
                return  dbSet.ToListAsync();
            }catch(Exception ex)
            {
                return null;
            }
            
            
        }

        public async Task<List<string>> GetAllCategoryNames()
        {
            List<string> categorynames = new List<string>();
            try
            {
                //START FROM EHRE THIS DOESNT WORK
                List<Category> categories = await this.GetAllAsync();
                foreach (var cat in categories)
                {
                    categorynames.Add(cat.Name);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Something went wrong", ex.Message);
            }
                return categorynames;
            
                 
        }

        public override async Task<Category> GetByIDAsync(int id)
        {
            return await dbSet.Include(x => x.Products).SingleOrDefaultAsync(c=>c.CategoryID == id);
        }

        public  Task<Category> GetByNameAsync(string name)
        {
            return  dbSet.Include(c=>c.Products).SingleOrDefaultAsync(c => c.Name == name);
        }

       
        
    }
   
}

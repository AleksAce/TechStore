using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{  
    //NOTE: This is GENERIC for all the ITEMS
    //Specific CRUD operations for the store implemented with the interface
    //More specific operations can be done in the StoreService Repository
    //If you wanna add more to the CRUD SPecific for a product or something. 
    //Add a Specific Interface
    public class StoreBaseRepository<T> : IStoreRepository<T> where T : class
    {
        protected StoreDBContext context = new StoreDBContext();

        private readonly DbSet<T> dbSet;
        public StoreBaseRepository()
        {
            //Get the specific set of the Required Table
            dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
        public async void DeleteByIDAsync(int id)
        {
            T entity = await dbSet.FindAsync(id);
            dbSet.Remove(entity);
        }
        public void Edit(T entity)
        {
            dbSet.Attach(entity);  
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetByIDAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }
       
        public async Task SaveAll()
        {
            await context.SaveChangesAsync();
        }

       
    }
}

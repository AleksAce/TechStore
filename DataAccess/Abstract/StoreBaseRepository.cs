using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{  
    //IMPORTANT: MUST Create specific repositories which can override the base methods.
    //IMPORTANT: Override base methods since Lazy Loading is disabled. or enable it in the constructor per service
    //Use the specific repository in your Specific SERVICE!
    public class StoreBaseRepository<T> : IStoreRepository<T> where T : class
    {
        protected StoreDBContext context = new StoreDBContext();

        protected DbSet<T> dbSet;
        public StoreBaseRepository()
        {
            //Get the specific set of the Required Table
            dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
            
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }
        
        public virtual void Edit(T entity)
        {
            dbSet.Attach(entity);  
        }
        public virtual Task<List<T>> GetAllAsync()
        {
            return  dbSet.ToListAsync();
        }

        public virtual async Task<T> GetByIDAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }
       



        public async Task SaveAll()
        {
            await context.SaveChangesAsync();
        }

        
    }
}

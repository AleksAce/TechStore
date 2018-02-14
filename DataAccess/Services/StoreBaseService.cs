using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public class StoreBaseService<T> :  IStoreService<T> where T : class
    {
        protected readonly IStoreRepository<T> repo;
        public StoreBaseService()
        {
            repo = new StoreBaseRepository<T>();
        }
        public StoreBaseService(IStoreRepository<T> storeRepository)
        {
            repo = storeRepository;
        }
       
        public void AddItem(T entity)
        {
            repo.Add(entity);
        }

        public void DeleteItem(T entity)
        {
            repo.Delete(entity);
        }

        public void DeleteItemByIDAsync(int id)
        {
            repo.DeleteByIDAsync(id);
        }

        public void EditItem(T entity)
        {
           repo.Edit(entity);
        }

        public async Task<List<T>> GetAllItemsAsync()
        {
            return await repo.GetAllAsync();
        }

        public async Task<T> GetItemByIDAsync(int id)
        {
            return await repo.GetByIDAsync(id);
        }

        public async Task SaveAllItems()
        {
            await repo.SaveAll();
        }
        
    }
}

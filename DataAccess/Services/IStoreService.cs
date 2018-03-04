using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public interface IStoreService<T> where T:class
    {
        void AddItem(T entity);
        void EditItem(T entity);
        void DeleteItem(T entity);
        Task DeleteItemByIDAsync(int id);
        Task<T> GetItemByIDAsync(int id);
        Task<List<T>> GetAllItemsAsync();
        Task SaveAllItemsAsync();
    }
}

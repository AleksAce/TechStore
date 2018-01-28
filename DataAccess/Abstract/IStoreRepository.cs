using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IStoreRepository<T> where T:class
    {
        void Add(T entity);
        void Edit(T entity);
        void Delete(T entity);
        void DeleteByIDAsync(int id);
        Task<T> GetByIDAsync(int id);
        Task<List<T>> GetAllAsync();
        Task SaveAll();
    }
}

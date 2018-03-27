using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICustomerRepository: IStoreRepository<Customer>
    {
        Task<Customer> GetProductByNameAsync(string UserName);
      
    }
    public class CustomerRepository : StoreBaseRepository<Customer>, ICustomerRepository
    {
        public async Task<Customer> GetProductByNameAsync(string UserName)
        {
            return await dbSet.SingleOrDefaultAsync(c => c.UserName == UserName);
        }
    }
}

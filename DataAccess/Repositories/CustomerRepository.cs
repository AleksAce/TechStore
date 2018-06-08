using Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Abstract;

namespace DataAccess.Repositories
{
    public class CustomerRepository : StoreBaseRepository<Customer>, ICustomerRepository
    {
        public Task<Customer> GetCustomerByNameAsync(string UserName)
        {
            return  dbSet.SingleOrDefaultAsync(c => c.UserName == UserName);
        }
    }
}

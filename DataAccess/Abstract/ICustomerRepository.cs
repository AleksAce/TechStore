using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.Abstract
{
    public interface ICustomerRepository : IStoreRepository<Customer>
    {
        Task<Customer> GetCustomerByNameAsync(string UserName);

    }
}

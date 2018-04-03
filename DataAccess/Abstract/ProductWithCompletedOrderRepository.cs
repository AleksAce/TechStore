using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
 
    public interface IProductWithCompletedOrderRepository : IStoreRepository<ProductWithCompletedOrder>
    {
 
    }
   

    public class ProductWithCompletedOrderRepository : StoreBaseRepository<ProductWithCompletedOrder>, IProductWithCompletedOrderRepository
    {
    
    }
}

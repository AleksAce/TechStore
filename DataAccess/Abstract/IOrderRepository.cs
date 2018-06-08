using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DataAccess.Abstract
{
    public interface IOrderRepository : IStoreRepository<Order>
    {
        Task CreateOrder(List<ProductWithCompletedOrder> ProductsForOrder, int CustomerID);
        Task AddProductToOrder(int ProductID, int OrderID);
        Task RemoveProductFromOrder(int ProductInfoID, int OrderID);
        Task AddCustomerToOrderAsync(int orderID, int customerID);
        Task RemoveCustomerFromOrderAsync(int orderID, int customerID);
        Task<List<Order>> GetAllOrdersPerProduct(int productID);
    }
}

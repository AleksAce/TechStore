using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace DataAccess.Abstract
{
    public class OrderRepository : StoreBaseRepository<Order>, IOrderRepository
    {
        public OrderRepository() : base()
        {

        }

        public async Task AddCustomerToOrderAsync(int orderID, int customerID)
        {
            try
            {
                Order order = await GetByIDAsync(orderID);
                Customer customer  = await context.Customers.FindAsync(customerID);
                order.customer = customer;
            }
            catch
            {
                throw;
            }
        }
        public async Task RemoveCustomerFromOrderAsync(int orderID, int customerID)
        {
            try
            {
                Order order = await GetByIDAsync(orderID);
                Customer customer = await context.Customers.FindAsync(customerID);
                customer.OrdersIssued.Remove(order);   
            }
            catch
            {
                throw;
            }

        }

        public async Task AddProductToOrder(int ProductID, int OrderID)
        {
            try
            {
                Order order = await GetByIDAsync(OrderID);
                Product product = await context.Products.FindAsync(ProductID);
                order.ProductsOrdered.Add(product);
            }
            catch
            {
                throw;
            }

        }
       
        //Creates an order with the products given
        public async Task CreateOrder(List<Product> ProductsForOrder, int CustomerID)
        {

            Order order = new Order();
            order.OrderDate = DateTime.Now;
            foreach (var prod in ProductsForOrder)
            {
                var ProductToOrder = await context.Products.FindAsync(prod.ProductID);
                order.ProductsOrdered.Add(ProductToOrder);
            }
            Customer CustomerOrdering = await context.Customers.FindAsync(CustomerID);
            order.customer = CustomerOrdering;
            Add(order);
           // context.Orders.Add(order);

        }

        public override Task<List<Order>> GetAllAsync()
        {
            return dbSet.Include(o => o.ProductsOrdered).ToListAsync();
          

        }
        public async override Task<Order> GetByIDAsync(int id)
        {
            return await dbSet.Include(o => o.ProductsOrdered).SingleOrDefaultAsync(o => o.OrderID == id);
        }

        
        public async Task RemoveProductFromOrder(int ProductID, int OrderID)
        {
            try
            {
                Order order = await GetByIDAsync(OrderID);
                Product product = await context.Products.FindAsync(ProductID);
                order.ProductsOrdered.Remove(product);
            }catch
            {
                throw;
            }

        }
    }
    public interface IOrderRepository : IStoreRepository<Order>
    {
        Task CreateOrder(List<Product> ProductsForOrder, int CustomerID);
        Task AddProductToOrder(int ProductID, int OrderID);
        Task RemoveProductFromOrder(int ProductID, int OrderID);
        Task AddCustomerToOrderAsync(int orderID, int customerID);
        Task RemoveCustomerFromOrderAsync(int orderID, int customerID);
    }

}


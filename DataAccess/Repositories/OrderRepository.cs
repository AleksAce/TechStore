using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DataAccess.Abstract;

namespace DataAccess.Repositories
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
                ProductWithCompletedOrder productOrderInfo = new ProductWithCompletedOrder()
                {
                    productID = ProductID,
                    PricePayed = product.Price,

                };
                order.ProductsOrderInfo.Add(productOrderInfo);
            }
            catch
            {
                throw;
            }

        }
       
        //Creates an order with the products given
        public async Task CreateOrder(List<ProductWithCompletedOrder> ProductsForOrder, int CustomerID)
        {

            Order order = new Order();
            order.OrderDate = DateTime.Now;
            foreach (var prodForOrder in ProductsForOrder)
            {
                order.ProductsOrderInfo.Add(prodForOrder);
            }
            Customer CustomerOrdering = await context.Customers.FindAsync(CustomerID);
            order.customer = CustomerOrdering;
            Add(order);
           // context.Orders.Add(order);

        }

        public override Task<List<Order>> GetAllAsync()
        {
            return dbSet.ToListAsync();
          

        }
        public async override Task<Order> GetByIDAsync(int id)
        {
            return await dbSet.SingleOrDefaultAsync(o => o.OrderID == id);
        }

        
        public async Task RemoveProductFromOrder(int ProductInfoID, int OrderID)
        {
            try
            {
                Order order = await GetByIDAsync(OrderID);
                ProductWithCompletedOrder productOrderInfo = await context.ProductsOrdered.FindAsync(ProductInfoID);
                order.ProductsOrderInfo.Remove(productOrderInfo);
            }catch
            {
                throw;
            }

        }

        public  Task<List<Order>> GetAllOrdersPerProduct(int productID)
        {
            return context.ProductsOrdered.Where(p => p.productID == productID).Select(p=>p.order).Distinct().ToListAsync();
           
            
        }
    }
    
}


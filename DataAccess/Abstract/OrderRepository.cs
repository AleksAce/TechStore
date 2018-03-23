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

        public async Task AddProductToOrder(int ProductID, int OrderID)
        {
            Order order = await GetByIDAsync(OrderID);
            Product product = await context.Products.FindAsync(ProductID);
            order.ProductsOrdered.Add(product);

        }

        public async Task CreateOrder(List<Product> ProductsForOrder)
        {

            Order order = new Order();
            order.OrderDate = DateTime.Now;

            foreach (var prod in ProductsForOrder)
            {
                var ProductToOrder = await context.Products.FindAsync(prod.ProductID);
                order.ProductsOrdered.Add(ProductToOrder);
            }
            context.Orders.Add(order);

        }

        public override async Task<List<Order>> GetAllAsync()
        {
            List<Order> orders = await dbSet.Include(o => o.ProductsOrdered).ToListAsync();
            return orders;

        }
        public async override Task<Order> GetByIDAsync(int id)
        {
            return await dbSet.Include(o => o.ProductsOrdered).SingleOrDefaultAsync(o => o.OrderID == id);
        }

        public async Task RemoveProductFromOrder(int ProductID, int OrderID)
        {
            Order order = await GetByIDAsync(OrderID);
            Product product = await context.Products.FindAsync(ProductID);
            order.ProductsOrdered.Remove(product);

        }
    }
    public interface IOrderRepository
    {
        Task CreateOrder(List<Product> ProductsForOrder);
        Task AddProductToOrder(int ProductID, int OrderID);
        Task RemoveProductFromOrder(int ProductID, int OrderID);
    }

}


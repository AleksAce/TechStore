using DataAccess.Abstract;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public interface IOrderService : IStoreService<Order>
    {
    }
    public class OrderService : IOrderService
    {
        private OrderRepository _orderRepository;

        public OrderService(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public void AddItem(Order entity)
        {
            _orderRepository.Add(entity);
        }

        public void DeleteItem(Order entity)
        {
            _orderRepository.Delete(entity);
        }

        public async Task DeleteItemByIDAsync(int id)
        {
            await _orderRepository.DeleteByIDAsync(id);
        }

        public void EditItem(Order entity)
        {
            _orderRepository.Edit(entity);
        }

        public async Task<List<Order>> GetAllItemsAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> GetItemByIDAsync(int id)
        {
            return await _orderRepository.GetByIDAsync(id);
        }

        public async Task SaveAllItemsAsync()
        {
            await _orderRepository.SaveAll();
        }
    }


}

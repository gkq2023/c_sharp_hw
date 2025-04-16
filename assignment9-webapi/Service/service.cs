using assignment9.Models;
using System.Collections.Generic;
using System.Linq;

namespace assignment9.Service
{
    public class OrderService
    {
        private readonly List<Order> _orders;

        public OrderService()
        {
            _orders = new List<Order>();
        }

        public List<Order> GetAllOrders()
        {
            return _orders.ToList();
        }

        public Order GetOrderById(int id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        public void AddOrder(Order order)
        {
            if (_orders.Any(o => o.Id == order.Id))
                throw new InvalidOperationException($"Order with ID {order.Id} already exists.");
            _orders.Add(order);
        }

        public void UpdateOrder(Order updatedOrder)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.Id == updatedOrder.Id);
            if (existingOrder == null)
                throw new KeyNotFoundException($"Order with ID {updatedOrder.Id} not found.");

            existingOrder.Customer = updatedOrder.Customer;
            existingOrder.Amount = updatedOrder.Amount;
            existingOrder.OrderDate = updatedOrder.OrderDate;
        }

        public void DeleteOrder(int id)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order == null)
                throw new KeyNotFoundException($"Order with ID {id} not found.");

            _orders.Remove(order);
        }

        public List<Order> SearchOrders(string customerName)
        {
            return _orders.Where(o => o.Customer.Contains(customerName, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
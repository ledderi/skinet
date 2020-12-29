using Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetOrdersAsync(string buyerEmail);
        Task<Order> GetOrderAsync(int orderId, string buyerEmail);
        Task<Order> CreateOrderAsync(DeliveryAddress deliveryAddress,int deliveryMethodId, string basketId, string buyerEmail);
        Task<Order> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrder(int orderId);
        Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}

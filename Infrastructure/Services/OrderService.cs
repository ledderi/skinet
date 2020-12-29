using Core.Entities;
using Core.Entities.Order;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepository, IUnitOfWork unitOfWork)
        {            
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Order> CreateOrderAsync(DeliveryAddress deliveryAddress, int deliveryMethodId, string basketId, string buyerEmail)
        {
            Order order = new Order { BuyerEmail = buyerEmail, DeliveryAddress = deliveryAddress };

            IEnumerable<Product> products = await _unitOfWork.Repository<Product>().GetAllItemsAsync();
            CustomerBasket customerBasket = await _basketRepository.GetBasketAsync(basketId);
            customerBasket.Items.ForEach(basketItem =>
            {
                Product product = products.FirstOrDefault(p => p.Id == basketItem.ProductId);
                order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    PictureUrl = product.PictureUrl,
                    Price = product.Price,
                    Quantity = basketItem.Quantity
                });
            });

            order.DeliveryMethodId = deliveryMethodId;
            order.DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetItemByIdAsync(deliveryMethodId);

            _unitOfWork.Repository<Order>().Add(order);
            await _unitOfWork.CompleteAsync();

            await _basketRepository.DeleteBasketAsync(basketId);

            return order;
        }

        public Task<bool> DeleteOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            IEnumerable<DeliveryMethod> deliveryMethods = await _unitOfWork.Repository<DeliveryMethod>().GetAllItemsAsync(); // _deliveryMethodRepository.GetAllItemsAsync();
            return deliveryMethods;
        }

        public async Task<Order> GetOrderAsync(int orderId, string buyerEmail)
        {
            OrderSpecification spec = new OrderSpecification(buyerEmail, orderId);
            Order order = await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);
            return order;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(string buyerEmail)
        {
            OrderSpecification spec = new OrderSpecification(buyerEmail);
            IEnumerable<Order> orders = await _unitOfWork.Repository<Order>().GetEntitiesWithSpecAsync(spec);
            return orders;
        }

        public Task<Order> UpdateOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }
    }
}

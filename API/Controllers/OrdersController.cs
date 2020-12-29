using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Order;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderRequestDto orderDto)
        {
            string buyerEmail = HttpContext.User.GetUserEmail();
            DeliveryAddress deliveryAddress = _mapper.Map<DeliveryAddress>(orderDto.DeliveryAddress);
            Order order = await _orderService.CreateOrderAsync(deliveryAddress, orderDto.DeliveryMethodId, orderDto.BasketId, buyerEmail);

            if (order == null)
            {
                return BadRequest(new Errors.ApiErrorResponse(400, "order creation faild"));
            }

            return Ok(order);
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<OrderResponseDto>> GetUserOrders()
        {
            string buyerEmail = HttpContext.User.GetUserEmail();
            IEnumerable<Order> orders = await _orderService.GetOrdersAsync(buyerEmail);
            IEnumerable<OrderResponseDto> response = _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
            return response;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponseDto>> GetUserOrder(int id)
        {
            string buyerEmail = HttpContext.User.GetUserEmail();
            Order order = await _orderService.GetOrderAsync(id, buyerEmail);

            if (order == null)
            {
                return NotFound(new ApiErrorResponse(404, $"order no. {id} was not found"));
            } else
            {
                OrderResponseDto response = _mapper.Map<OrderResponseDto>(order);
                return Ok(response);
            }
        }

        [HttpGet("deliveryMethods")]
        public async Task<IEnumerable<DeliveryMethod>> GetDeliveryMethods()
        {
            IEnumerable<DeliveryMethod> deliveryMethods = await _orderService.GetDeliveryMethodsAsync();
            return deliveryMethods;
        }
    }
}

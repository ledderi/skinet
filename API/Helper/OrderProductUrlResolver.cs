using API.Dtos;
using AutoMapper;
using Core.Entities.Order;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helper
{
    public class OrderProductUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;

        public OrderProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                result = $"{ _configuration.GetValue<string>("ApiUrl") }{ source.PictureUrl }";
            }

            return result;
        }
    }
}

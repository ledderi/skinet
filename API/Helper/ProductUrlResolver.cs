using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helper
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
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

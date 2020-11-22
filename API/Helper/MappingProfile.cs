using API.Dtos;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(p => p.ProductType, src => src.MapFrom(p => p.ProductType.Name))
                .ForMember(p => p.ProductBrand, src => src.MapFrom(p => p.ProductBrand.Name))
                .ForMember(p => p.PictureUrl, src => src.MapFrom<ProductUrlResolver>());
        }
    }
}

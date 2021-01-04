using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.Order;
using System;

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

            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
            CreateMap<CustomerBasket, CustomerBasketDto>().ReverseMap();
            CreateMap<AddressDto, DeliveryAddress>();

            CreateMap<Order, OrderResponseDto>()
                .ForMember(p => p.OrderItems, src => src.MapFrom(p => p.Items))
                .ForMember(p => p.DeliveryMethod, src => src.MapFrom(p => p.DeliveryMethod.ShortName))
                .ForMember(p => p.DeliveryMethodId, src => src.MapFrom(p => p.DeliveryMethod.Id))
                .ForMember(p => p.ShippingPrice, src => src.MapFrom(p => p.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(p => p.PictureUrl, src => src.MapFrom<OrderProductUrlResolver>());
            CreateMap<DeliveryAddress, AddressDto>();

            CreateMap<AppUser, UserDto>()
                .ForMember(p => p.Token, src => src.MapFrom<TokenResolver>());
        }
    }
}

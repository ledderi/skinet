using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;

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

            CreateMap<AppUser, UserDto>()
                .ForMember(p => p.Token, src => src.MapFrom<TokenResolver>());
        }
    }
}

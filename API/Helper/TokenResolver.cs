using API.Dtos;
using AutoMapper;
using Core.Entities.Identity;
using Core.Interfaces;

namespace API.Helper
{
    public class TokenResolver : IValueResolver<AppUser, UserDto, string>
    {
        private readonly ITokenService _tokenService;

        public TokenResolver(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public string Resolve(AppUser source, UserDto destination, string destMember, ResolutionContext context)
        {
            return _tokenService.CreateToken(source);
        }
    }
}

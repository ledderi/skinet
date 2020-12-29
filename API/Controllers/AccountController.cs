using API.Dtos;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            string email = HttpContext.User.GetUserEmail();
            AppUser user = await _userManager.FindByEmailAsync(email);
            UserDto userDto = _mapper.Map<AppUser, UserDto>(user);
            return Ok(userDto);
        }

        [HttpGet("emailexist")]
        public async Task<ActionResult<bool>> CheckEmailExist([FromQuery] string email)
        {
            bool exist = await _userManager.FindByEmailAsync(email) != null;
            return Ok(exist);
        }

        [Authorize]
        [HttpGet("address")]
        public async Task<ActionResult<AddressDto>> GetUserAddress()
        {
            string email = HttpContext.User.GetUserEmail();
            AppUser user = await _userManager.Users.Include(p => p.Address).SingleOrDefaultAsync(p => p.Email == email);
            AddressDto address = _mapper.Map<Address, AddressDto>(user.Address);
            return Ok(address);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto oldAddress)
        {
            string email = HttpContext.User.GetUserEmail();
            AppUser user = await _userManager.Users.Include(p => p.Address).SingleOrDefaultAsync(p => p.Email == email);
            user.Address = _mapper.Map<AddressDto, Address>(oldAddress);
            IdentityResult identityResult = await _userManager.UpdateAsync(user);

            if (identityResult.Succeeded)
            {
                return Ok(_mapper.Map<Address, AddressDto>(user.Address));
            }
            else
            {
                return BadRequest(new ApiErrorResponse(400, "updating user address failed"));
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            AppUser user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return Unauthorized(new ApiErrorResponse(401));
            }

            bool passwordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!passwordValid)
            {
                return Unauthorized(new ApiErrorResponse(401));
            }

            UserDto userDto = _mapper.Map<AppUser, UserDto>(user);
            return Ok(userDto);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            AppUser user = await _userManager.FindByEmailAsync(registerDto.Email);

            if (user != null)
            {
                return BadRequest(new ApiErrorResponse(400, "email all ready exist!"));
            }

            user = new AppUser { Email = registerDto.Email, DisplayName = registerDto.DisplayName, UserName = registerDto.Email };

            IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiErrorResponse(400, "user registration faild!"));
            }

            UserDto userDto = _mapper.Map<AppUser, UserDto>(user);
            return Ok(userDto);
        }
    }
}

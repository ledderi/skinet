using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<CustomerBasketDto> GetCustomerBasket([FromQuery] string basketId)
        {
            CustomerBasket customerBasket = await _basketRepository.GetBasketAsync(basketId);
            CustomerBasketDto customerBasketDto = _mapper.Map<CustomerBasket, CustomerBasketDto>(customerBasket);
            return customerBasketDto;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> SetCustomerBasket(CustomerBasketDto basket)
        {
            CustomerBasket customerBasket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basket);
            customerBasket = await _basketRepository.AddOrUpdateBasketAsync(customerBasket);
            CustomerBasketDto customerBasketDto = _mapper.Map<CustomerBasket, CustomerBasketDto>(customerBasket);
            return Ok(customerBasketDto);
        }

        [HttpDelete("{basketId}")]
        public async Task<ActionResult<bool>> DeleteBasket(string basketId)
        {
            bool deleted = await _basketRepository.DeleteBasketAsync(basketId);
            return Ok(deleted);
        }
    }
}

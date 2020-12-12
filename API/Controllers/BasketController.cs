using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        [HttpGet]
        public async Task<CustomerBasket> GetCustomerBasket([FromQuery] string basketId)
        {
            return await _basketRepository.GetBasketAsync(basketId);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> SetCustomerBasket(CustomerBasket basket)
        {
            CustomerBasket customerBasket = await _basketRepository.AddOrUpdateBasketAsync(basket);
            return Ok(customerBasket);
        }

        [HttpDelete("{basketId}")]
        public async Task<ActionResult<bool>> DeleteBasket(string basketId)
        {
            bool deleted = await _basketRepository.DeleteBasketAsync(basketId);
            return Ok(deleted);
        }
    }
}

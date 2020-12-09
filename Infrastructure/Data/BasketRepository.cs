using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<CustomerBasket> AddOrUpdateBasketAsync(CustomerBasket basket)
        {
            string data = JsonSerializer.Serialize<CustomerBasket>(basket);
            return await _database.StringSetAsync(basket.Id, data, TimeSpan.FromDays(30)) ? basket : null;
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            CustomerBasket basket = null;

            if (await _database.KeyExistsAsync(basketId))
            {
                RedisValue redisValue = await _database.StringGetAsync(basketId);
                basket = JsonSerializer.Deserialize<CustomerBasket>(redisValue);
            }

            return basket;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using StackExchange.Redis;

namespace Persistence.Data.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {

        private readonly IDatabase _database = connection.GetDatabase();

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var redisValue = await _database.StringGetAsync(id);

            if (redisValue.IsNullOrEmpty)
                return null;

            var basket = JsonSerializer.Deserialize<CustomerBasket>(redisValue);

            if (basket is null)
                return null;

            return basket;

        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            var ridesValue = JsonSerializer.Serialize(basket);
            var flag = _database.StringSet(basket.Id, ridesValue, TimeSpan.FromDays(30));

                return flag ? await GetBasketAsync(basket.Id) : null;

        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

      
      
    }
}

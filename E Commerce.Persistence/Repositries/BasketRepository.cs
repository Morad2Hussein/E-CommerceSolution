
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Basket_Module;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Text.Json;

namespace E_Commerce.Persistence.Repositries
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _database = connectionMultiplexer.GetDatabase();
        }
        #region Create Or Update 
        public async Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket customerBasket, TimeSpan timeToLive = default)
        {
            var JsonBasket = JsonSerializer.Serialize(customerBasket);
            var IsCeratedOrUpdated = await _database.StringSetAsync(
                                    customerBasket.Id, JsonBasket,
                                    (timeToLive == default) ? TimeSpan.FromDays(7) : timeToLive);
            if (IsCeratedOrUpdated)
            {
                return await GetBasketAsync(customerBasket.Id);
            }
            else
            {
                return null;
            }



        }

        #endregion
        #region Get Customer Basket
        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var Basket =await  _database.StringGetAsync(basketId);
            if(Basket.IsNullOrEmpty)
                return null;
            else 
                return  JsonSerializer.Deserialize<CustomerBasket> (Basket!);
        }

        #endregion
        public async Task<bool> DeleteBasketAsync(string basketId) =>await  _database.KeyDeleteAsync(basketId);

    }
}

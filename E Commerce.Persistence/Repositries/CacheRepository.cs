using E_Commerce.Domain.Contracts;
using StackExchange.Redis;

namespace E_Commerce.Persistence.Repositries
{
    public class CacheRepository : ICacheRepository
    { 
        private readonly IDatabase _database;
        public CacheRepository( IConnectionMultiplexer multiplexer)
        {
            _database = multiplexer.GetDatabase();
        }
        public async Task<string?> GetAsync(string Cachekey)
        {
            var value = await    _database.StringGetAsync(Cachekey);
                return value.IsNullOrEmpty ? null : value.ToString();
        }

        public async Task SetAsync(string Cachekey, string value, TimeSpan TimeToLive)
        {
               await  _database.StringSetAsync(Cachekey, value, TimeToLive);
        }
    }
}

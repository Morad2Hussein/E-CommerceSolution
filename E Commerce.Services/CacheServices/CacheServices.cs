

using E_Commerce.Domain.Contracts;
using E_Commerce.Services_Abstraction.Services;
using System.Text.Json;

namespace E_Commerce.Services.CacheServices
{
    public class CacheServices : ICacheServices
    {
        private readonly ICacheRepository _repository;
        public CacheServices( ICacheRepository repository)
        {
            _repository = repository;
        }
        public async Task<string?> GetAsync(string CacheKey)
        {
            return await  _repository.GetAsync(CacheKey);
        }

       
        public async Task SetAsync(string CacheKey, object value, TimeSpan TimeToLive)
        {
          var Value = JsonSerializer.Serialize(value);
           await  _repository.SetAsync(CacheKey, Value, TimeToLive);
        }
    }
}

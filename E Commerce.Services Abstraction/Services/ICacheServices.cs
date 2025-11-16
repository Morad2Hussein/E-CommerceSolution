namespace E_Commerce.Services_Abstraction.Services
{
    public interface ICacheServices
    {
        Task<string?> GetAsync(string CacheKey);
        Task SetAsync(string CacheKey, object value, TimeSpan TimeToLive);
    
    }
}

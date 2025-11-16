namespace E_Commerce.Domain.Contracts
{
    public interface ICacheRepository
    {
        Task<string?> GetAsync(string Cachekey);
        Task SetAsync(string Cachekey, string value, TimeSpan TimeToLive);
    }
}

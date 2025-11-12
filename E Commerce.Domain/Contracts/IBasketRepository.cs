

using E_Commerce.Domain.Entities.Basket_Module;

namespace E_Commerce.Domain.Contracts
{
    public interface IBasketRepository
    {



        Task<CustomerBasket?> GetBasketAsync(string basketId);

        Task<CustomerBasket?> CreateOrUpdateBasketAsync(CustomerBasket customerBasket , TimeSpan timeToLive=default);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}



using E_Commerce.Shared.DTOS.BasketDTO;

namespace E_Commerce.Services_Abstraction.Services
{
    public interface IBasketServices
    {
        Task<BasketDTO> GetBasketAysnc(string Id);
        Task<BasketDTO> CreateOrUpdateBasketAsync(BasketDTO basket);
        Task<bool> DeleteBasketAsync(string Id);
    }
}

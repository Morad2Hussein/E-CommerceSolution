

namespace E_Commerce.Shared.DTOS.BasketDTO
{
    public record BasketDTO(string Id ,ICollection<BasketItemDTO> items);
}


using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Shared.DTOS.BasketDTO
{
    public record BasketItemDTO
                (
                    int Id,
                    string ProductName,
                    string PictureUrl,
                    [Range(1,double.MaxValue)]
                    decimal Price,
                    [Range(1,100)]
                    int Quantity);
}




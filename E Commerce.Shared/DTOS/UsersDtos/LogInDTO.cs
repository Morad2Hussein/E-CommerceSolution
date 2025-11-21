
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Shared.DTOS.UsersDtos
{
   public record LogInDTO([EmailAddress] string Email, string Password);
    
    
}

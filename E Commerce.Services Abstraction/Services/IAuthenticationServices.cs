
using E_Commerce.Shared.ComonResults;
using E_Commerce.Shared.DTOS.UsersDtos;

namespace E_Commerce.Services_Abstraction.Services
{
    public interface IAuthenticationServices
    {
        #region Login 
        // Email , Password  =|> return JWT Token = DisplayName , Email
         Task<Results<UserDTO>> LoginAsync(LogInDTO logInDTO);
        #endregion

        #region Register
        // Email , Password , UserName, PhoneNumber ,DisplayName =|> return Email , DisplayName , JWT Token
        Task<Results<UserDTO>> RegisterAsync(RegisterDTO registerDTO);


        #endregion
    }
}


using E_Commerce.Domain.Entities.IdentityModule;
using E_Commerce.Services_Abstraction.Services;
using E_Commerce.Shared.ComonResults;
using E_Commerce.Shared.DTOS.UsersDtos;
using Microsoft.AspNetCore.Identity;

namespace E_Commerce.Services.AuthenticationServices
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthenticationServices(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<Results<UserDTO>> LoginAsync(LogInDTO logInDTO)
        {
            var user = await _userManager.FindByEmailAsync(logInDTO.Email);
            if (user == null)
                return Errors.InValidCredentials("User.InValidCredentials");
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, logInDTO.Password);
            if (!isPasswordValid)

                return Errors.InValidCredentials("User.InValidCredentials");

            return new UserDTO(user.DisplayName, user.Email!, "Token");



        }

        public async Task<Results<UserDTO>> RegisterAsync(RegisterDTO registerDTO)
        {
            var user = new ApplicationUser
            {
                DisplayName = registerDTO.DisplayName,
                Email = registerDTO.Email,
                UserName = registerDTO.UserName,
                PhoneNumber = registerDTO.PhoneNumber,
            };

            var IdentityResult = await _userManager.CreateAsync(user, registerDTO.Password);
            if (IdentityResult.Succeeded)
                return new UserDTO(user.DisplayName, user.Email!, "Token");
            return IdentityResult.Errors.Select(e => Errors.Vaildation(e.Code, e.Description)).ToList();
        }
    }
}

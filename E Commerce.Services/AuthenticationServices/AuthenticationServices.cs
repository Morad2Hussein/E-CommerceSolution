using E_Commerce.Domain.Entities.IdentityModule;
using E_Commerce.Services_Abstraction.Services;
using E_Commerce.Shared.ComonResults;
using E_Commerce.Shared.DTOS.UsersDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Services.AuthenticationServices
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public AuthenticationServices(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        #region Login
        public async Task<Results<UserDTO>> LoginAsync(LogInDTO logInDTO)
        {
            var user = await _userManager.FindByEmailAsync(logInDTO.Email);
            if (user == null)
                return Errors.InValidCredentials("User.InValidCredentials");
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, logInDTO.Password);
            if (!isPasswordValid)

                return Errors.InValidCredentials("User.InValidCredentials");
            var Token = await CreateToken(user);

            return new UserDTO(user.DisplayName, user.Email!, Token);



        }
        #endregion
        #region Register 

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
            {
                var Token = await CreateToken(user);
                return new UserDTO(user.DisplayName, user.Email!, Token);
            }
            return IdentityResult.Errors.Select(e => Errors.Vaildation(e.Code, e.Description)).ToList();
        }
        #endregion

        #region   Create Token by JWT
        private async Task<string> CreateToken(ApplicationUser user)
        {
            // ============================================
            // 1. Build the list of claims for the JWT token
            // ============================================

            // Claims represent user information stored inside the token.
            // These two claims store the user's email and username.
            var Cliams = new List<Claim>()
                            {
                                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                                new Claim(JwtRegisteredClaimNames.Name, user.UserName!),
                            };

            // ============================================
            // 2. Add user roles as claims inside the token
            // ============================================

            // Get all roles assigned to the current user from Identity
            var Roles = await _userManager.GetRolesAsync(user);

            // For each role, add a Role claim to the token.
            // This allows authorization based on roles later.
            foreach (var role in Roles)
            {
                Cliams.Add(new Claim(ClaimTypes.Role, role));
            }

            // ============================================
            // 3. Prepare the signing key and credentials
            // ============================================

            // This is the secret key used to sign the token.
            // It MUST be long and stored safely in appsettings.json,
            // NOT hard-coded in real applications.
            var SecretKey = _configuration["JWTSettings:SecretKey"];

            // Convert the secret key into bytes and build the security key.
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            // Define the signing method (HMAC SHA256)
            var Cred = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            // ============================================
            // 4. Build the JWT token descriptor (settings)
            // ============================================

            var Token = new JwtSecurityToken
            (
                 issuer: _configuration["JwtSettings:Issuer"],
                  audience: _configuration["JwtSettings:Audience"],
                // Who can use the token
                expires: DateTime.UtcNow.AddHours(1), // Token expiration (1 hour)
                claims: Cliams,                       // Additional claim container
                signingCredentials: Cred             // Add signing credentials
            );



            // ============================================
            // 5. Create and return the encoded JWT string
            // ============================================

            // Convert the token descriptor into a signed JWT string
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
        #endregion
        #region Check email

        public async Task<bool> CheckEmailAsync(string email)
        {
            var User = await _userManager.FindByEmailAsync(email);
            return User != null;
        }

        public async Task<Results<UserDTO>> GetUserByEmailAsync(string email)
        {
           var User = await _userManager.FindByEmailAsync(email);
            if (User == null)
                return Errors.NotFound("User.NotFound", $"No Email {email} Found");
            return new UserDTO(User.Email!, User.DisplayName, await  CreateToken(User));
        }


        #endregion
    }
}

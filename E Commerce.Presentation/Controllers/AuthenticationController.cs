
using E_Commerce.Services_Abstraction.Services;
using E_Commerce.Shared.DTOS.UsersDtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace E_Commerce.Presentation.Controllers
{
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationServices _authenticationService;
        public AuthenticationController(IAuthenticationServices authenticationService)
        {
            _authenticationService = authenticationService;
        }

        #region Login EndPoint

        [HttpPost("Login")]
       
         public async  Task<ActionResult<UserDTO>> Login (LogInDTO logInDTO)
        {
            var Result = await _authenticationService.LoginAsync(logInDTO);
            return HandleResult(Result);
        }

        #endregion

        #region Register EndPoint
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register (RegisterDTO registerDTO)
        {
            var Result = await _authenticationService.RegisterAsync(registerDTO);
            return HandleResult(Result);
        }
        #endregion
        #region Check Email Exists
        // api/authentication/emailExists?email=
        [HttpGet("emailExists")]
      public async Task<ActionResult<bool>> CheckEmail(String email)
        {
            var Result = await _authenticationService.CheckEmailAsync(email);
            return Ok(Result);
        }
        // api/authentication/currentUser
        [Authorize]
        [HttpGet("CurrentUser")]
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var Result = await _authenticationService.GetUserByEmailAsync(Email);

            return HandleResult(Result);
        }
        #endregion
    }
}
            
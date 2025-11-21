
using E_Commerce.Services_Abstraction.Services;
using E_Commerce.Shared.DTOS.UsersDtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<UserDTO>> Register (RegisterDTO registerDTO)
        {
            var Result = await _authenticationService.RegisterAsync(registerDTO);
            return HandleResult(Result);
        }
        #endregion
    }
}

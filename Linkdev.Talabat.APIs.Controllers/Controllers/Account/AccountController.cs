using Linkdev.Talabat.APIs.Controllers.Controllers.Base;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Linkdev.Talabat.APIs.Controllers.Controllers.Account
{
    public class AccountController(IServiceManager serviceManager) : BaseApiController
    {
        [HttpPost("login")] // POST: "/api/Account/login"
        public async Task<ActionResult<UserDto>> Login(LoginDto user)
        {
            var data = await serviceManager.AuthService.LoginAsync(user);
            return Ok(data);
        }

        [HttpPost("Register")] // POST: "/api/Account/login"
        public async Task<ActionResult<UserDto>> Register(RegisterDto user)
        {
            var data = await serviceManager.AuthService.RegisterAsync(user);
            return Ok(data);
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet] // GET: /api/account
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            return Ok(await serviceManager.AuthService.GetCurrentUser(User));
        }
    }
}

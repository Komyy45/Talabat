using System.Security.Claims;
using Linkdev.Talabat.APIs.Controllers.Controllers.Base;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts;
using Linkdev.Talabat.Core.Application.Abstraction.Models._Common;
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
            return Ok(await serviceManager.AuthService.GetCurrentUserAsync(User));
        }

		[Authorize(AuthenticationSchemes = "Bearer")]
		[HttpGet("Address")] // GET: /api/account/address
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            return Ok(await serviceManager.AuthService.GetCurrentUserAddressAsync(User));
        }

		[Authorize(AuthenticationSchemes = "Bearer")]
		[HttpPut("Address")] // PUT: /api/account/address
        public async Task<ActionResult<AddressDto>> UpdateUserAddress(AddressDto addressDto)
        {
            return Ok(await serviceManager.AuthService.UpdateUserAddressAsync(User, addressDto));
        }

        [HttpGet("EmailExists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return Ok(await serviceManager.AuthService.EmailExistsAsync(email));
        }
    }
}

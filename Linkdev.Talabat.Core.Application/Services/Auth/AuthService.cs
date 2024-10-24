using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Auth;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Auth;
using Linkdev.Talabat.Core.Application.Exceptions;
using Linkdev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Linkdev.Talabat.Core.Application.Services.Auth
{
    internal class AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IAuthService
    {
        public async Task<UserDto> LoginAsync(LoginDto user)
        {
            var desiredUser = await userManager.FindByEmailAsync(user.Email);

            if (desiredUser == null) throw new UnAuthorizedException();

            var result = await signInManager.CheckPasswordSignInAsync(desiredUser, user.Password, true);

            if(result.IsNotAllowed) throw new UnAuthorizedException("Account has not been confirmed Yet");

            if(result.IsLockedOut) throw new UnAuthorizedException("This Account is Locked!");

            if(!result.Succeeded) throw new UnAuthorizedException("Invalid Login");

            return new UserDto()
            {
                Id = desiredUser.Id,
                DisplayName = desiredUser.DisplayName,
                Email = desiredUser.Email!,
                Token = "Test"
            };
        }

        public async Task<UserDto> RegisterAsync(RegisterDto user)
        {
            ApplicationUser applicationUser = new ApplicationUser()
            {
                UserName = user.UserName,
                Email = user.Email,
                DisplayName = user.DisplayName,
                PhoneNumber = user.PhoneNumber,
            };

            var result = await userManager.CreateAsync(applicationUser, user.Password);

            if (!result.Succeeded) new ValidationErrorException("BadRequest") { Errors = result.Errors.Select(E => E.Description) };

            return new UserDto()
            {
                Id = applicationUser.Id,
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = "Test"
            };
        }
    }
}

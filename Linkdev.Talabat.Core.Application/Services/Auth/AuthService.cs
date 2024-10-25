using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Auth;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Auth;
using Linkdev.Talabat.Core.Application.Exceptions;
using Linkdev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Linkdev.Talabat.Core.Application.Services.Auth
{
    public class AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : IAuthService
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
                Token = await GetJwtTokenAsync(desiredUser)
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
                Token = await GetJwtTokenAsync(applicationUser),
            };
        }

        public async Task<string> GetJwtTokenAsync(ApplicationUser applicationUser)
        {
            var userClaims = await userManager.GetClaimsAsync(applicationUser);
            var roles = await userManager.GetRolesAsync(applicationUser);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.PrimarySid, applicationUser.Id),
                new Claim(ClaimTypes.Email, applicationUser.Email!),
                new Claim(ClaimTypes.GivenName, applicationUser.DisplayName)
            }.Union(userClaims)
             .Union(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var secretKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes("My-Secret-Key"));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var jsonWebToken = new JwtSecurityToken(
                issuer: "Talabat.Apis",
                audience: "Talabat Users",
                expires: DateTime.UtcNow.AddMinutes(10),
                claims: claims,
                signingCredentials: signingCredentials
                );
            {
                
            };

            return new JwtSecurityTokenHandler().WriteToken(jsonWebToken);
        }
    }
}

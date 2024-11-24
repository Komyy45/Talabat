using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Auth;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Auth;
using Linkdev.Talabat.Core.Application.Exceptions;
using Linkdev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Linkdev.Talabat.Core.Application.Services.Auth
{
    public class AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtSettings) : IAuthService
    {
        public JwtSettings _jwtSettings { get; set; } = jwtSettings.Value;

		public async Task<UserDto> GetCurrentUser(ClaimsPrincipal claims)
		{
		    var user = await userManager.FindByEmailAsync(claims.FindFirstValue(ClaimTypes.Email)!);

			return new UserDto()
			{
				Id = user!.Id,
				DisplayName = user.DisplayName,
				Email = user.Email!,
                Token = await GetJwtTokenAsync(user),
			};
		}

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

        private async Task<string> GetJwtTokenAsync(ApplicationUser applicationUser)
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

            var secretKey =  new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var jsonWebToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                claims: claims,
                signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jsonWebToken);
        }
    }
}

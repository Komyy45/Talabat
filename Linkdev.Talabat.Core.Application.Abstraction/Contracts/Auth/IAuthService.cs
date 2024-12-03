using System.Security.Claims;
using Linkdev.Talabat.Core.Application.Abstraction.Models._Common;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Auth;

namespace Linkdev.Talabat.Core.Application.Abstraction.Contracts.Auth
{
	public interface IAuthService
    {
        public Task<UserDto> LoginAsync(LoginDto user);
        public Task<UserDto> RegisterAsync(RegisterDto user);
        public Task<UserDto> GetCurrentUserAsync(ClaimsPrincipal claims);
        public Task<AddressDto?> GetCurrentUserAddressAsync(ClaimsPrincipal claims);
        public Task<AddressDto> UpdateUserAddressAsync(ClaimsPrincipal claims, AddressDto updatedAddress);
        public Task<bool> EmailExistsAsync(string email);
    }
}

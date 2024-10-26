using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Auth;

namespace Linkdev.Talabat.Core.Application.Abstraction.Contracts.Auth
{
    public interface IAuthService
    {
        public Task<UserDto> LoginAsync(LoginDto user);
        public Task<UserDto> RegisterAsync(RegisterDto user);
    }
}

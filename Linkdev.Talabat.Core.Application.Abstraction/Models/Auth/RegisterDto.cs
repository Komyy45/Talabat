using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkdev.Talabat.Core.Application.Abstraction.Models.Auth
{
    public class RegisterDto
    {          
        public required string DisplayName { get; set; }
               
        public required string UserName { get; set; }
               
        public required string Email { get; set; }
               
        public required string Password { get; set; }

        public required string PhoneNumber { get; set; }
    }
}

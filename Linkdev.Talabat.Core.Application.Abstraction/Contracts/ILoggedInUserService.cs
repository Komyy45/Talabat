using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linkdev.Talabat.Core.Application.Abstraction.Contracts
{
    public interface ILoggedInUserService
    {
        public string? UserId { get; }
    }
}

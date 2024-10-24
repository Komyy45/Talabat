using Microsoft.AspNetCore.Identity;

namespace Linkdev.Talabat.Core.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public required string DisplayName { get; set; }

        public virtual Address? Address { get; set; }
    }
}

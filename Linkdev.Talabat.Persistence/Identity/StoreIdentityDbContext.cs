using Linkdev.Talabat.Core.Domain.Entities.Identity;
using Linkdev.Talabat.Persistence.Identity.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Linkdev.Talabat.Persistence.Identity
{
    public class StoreIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new ApplicationUserConfigurations());
            builder.ApplyConfiguration(new AddressConfigurations());
        }
    }
}

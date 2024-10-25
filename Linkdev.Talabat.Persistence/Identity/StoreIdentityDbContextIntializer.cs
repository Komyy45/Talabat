using Castle.Core.Logging;
using Linkdev.Talabat.Core.Domain.Contracts.Persistence.Intializers;
using Linkdev.Talabat.Core.Domain.Entities.Identity;
using Linkdev.Talabat.Persistence._Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Linkdev.Talabat.Persistence.Identity
{
    public class StoreIdentityDbContextIntializer(StoreIdentityDbContext dbContext, UserManager<ApplicationUser> userManager) : DbIntializer(dbContext), IStoreIdentityDbContextIntializer
    {
        public override async Task SeedAsync()
        {
            if(!dbContext.Users.Any())
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = "YoussefMohamed",
                    Email = "YoussefElkomy@gmail.com",
                    DisplayName = "Youssef_Elkomy",
                    PhoneNumber = "1157197362"
                };

                var result = await userManager.CreateAsync(user, "P@ssword1");

            }
        }
    }
}

using Linkdev.Talabat.Core.Domain.Contracts.Persistence.Intializers;
using Linkdev.Talabat.Core.Domain.Entities.Identity;
using Linkdev.Talabat.Persistence._Common;
using Microsoft.AspNetCore.Identity;

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
                    UserName = "Youssef Mohamed",
                    Email = "YoussefElkomy@gmail.com",
                    DisplayName = "Youssef_Elkomy",
                    PhoneNumber = "1157197362"
                };

                await userManager.CreateAsync(user, "1234");
            }
        }
    }
}

using Linkdev.Talabat.Core.Domain.Entities.Identity;
using Linkdev.Talabat.Persistence.Identity;
using Microsoft.AspNetCore.Identity;

namespace Linkdev.Talabat.APIs.Extensions
{
    public static class IdentityExtentions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(identityOptions =>
            {
                identityOptions.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz123456789*^&$#!";
                identityOptions.User.RequireUniqueEmail = true;
                
                identityOptions.SignIn.RequireConfirmedAccount = true;
                identityOptions.SignIn.RequireConfirmedEmail = true;
                identityOptions.SignIn.RequireConfirmedPhoneNumber = true;
                
                
                identityOptions.Password.RequireNonAlphanumeric = true;
                identityOptions.Password.RequiredUniqueChars = 2;
                identityOptions.Password.RequireDigit = true;
                identityOptions.Password.RequiredLength = 6;
                identityOptions.Password.RequireLowercase = true;
                identityOptions.Password.RequireUppercase = true;

                identityOptions.Lockout.AllowedForNewUsers = true;
                identityOptions.Lockout.MaxFailedAccessAttempts = 5;
                identityOptions.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
            })
                    .AddEntityFrameworkStores<StoreIdentityDbContext>();

            return services;
        }
    }
}

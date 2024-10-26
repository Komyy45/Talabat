using System.Text;
using Linkdev.Talabat.Core.Application.Abstraction.Contracts.Auth;
using Linkdev.Talabat.Core.Application.Abstraction.Models.Auth;
using Linkdev.Talabat.Core.Application.Services.Auth;
using Linkdev.Talabat.Core.Domain.Entities.Identity;
using Linkdev.Talabat.Persistence.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Linkdev.Talabat.APIs.Extensions
{
    public static class IdentityExtentions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {

                bearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    
                    ClockSkew = TimeSpan.FromMinutes(0),
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!))
                };
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(identityOptions =>
            {
                // identityOptions.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz123456789*^&$#!";
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

            services.AddScoped(typeof(IAuthService), typeof(AuthService));
            services.AddScoped(typeof(Func<IAuthService>), serviceProvider => () => serviceProvider.GetRequiredService<IAuthService>());

            return services;
        }
    }
}

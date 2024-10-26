using System.Data;
using Linkdev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Linkdev.Talabat.Persistence.Identity.Config
{
    [DbContext(typeof(StoreIdentityDbContext))]
    internal class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(A => A.DisplayName)
                   .HasColumnType(nameof(SqlDbType.NVarChar))
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasOne(A => A.Address)
                   .WithOne(A => A.User)
                   .HasForeignKey<Address>(A => A.UserId);
        }
    }
}

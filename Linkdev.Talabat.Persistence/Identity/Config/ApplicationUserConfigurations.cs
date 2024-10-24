using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Linkdev.Talabat.Core.Domain.Entities.Identity;

namespace Linkdev.Talabat.Persistence.Identity.Config
{
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

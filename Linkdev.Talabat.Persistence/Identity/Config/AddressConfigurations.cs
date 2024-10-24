﻿using System.Data;
using Linkdev.Talabat.Core.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Linkdev.Talabat.Persistence.Identity.Config
{
    [DbContext(contextType: typeof(StoreIdentityDbContext))]
    internal class AddressConfigurations : IEntityTypeConfiguration<Address>
    {

        public void Configure(EntityTypeBuilder<Address> builder)
        {

            builder.Property(A => A.Id)
                   .UseIdentityColumn(10,10)
                   .IsRequired();

            builder.Property(A => A.FirstName).HasColumnType(nameof(SqlDbType.NVarChar)).HasMaxLength(100);
            builder.Property(A => A.LastName).HasColumnType(nameof(SqlDbType.NVarChar)).HasMaxLength(100);
            builder.Property(A => A.Street).HasColumnType(nameof(SqlDbType.NVarChar)).HasMaxLength(100);
            builder.Property(A => A.City).HasColumnType(nameof(SqlDbType.NVarChar)).HasMaxLength(100);
            builder.Property(A => A.Country).HasColumnType(nameof(SqlDbType.NVarChar)).HasMaxLength(100);

            builder.ToTable("Addresses");
        }
    }
}

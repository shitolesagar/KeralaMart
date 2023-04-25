using KeralaMiniMart.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Repository.Configurations
{
    internal class UserDeviceInfoConfiguration : IEntityTypeConfiguration<UserDeviceInfo>
    {
        public void Configure(EntityTypeBuilder<UserDeviceInfo> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.Property(x => x.DeviceId);

            builder.HasOne(x => x.ApplicationUser).WithMany(x => x.UserDeviceInfos).HasForeignKey(x => x.ApplicationUserId);
        }
    }
}

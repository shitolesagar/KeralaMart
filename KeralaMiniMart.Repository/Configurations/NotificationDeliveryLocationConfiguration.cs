using KeralaMiniMart.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Repository.Configurations
{
    internal class NotificationDeliveryLocationConfiguration : IEntityTypeConfiguration<NotificationDeliveryLocation>
    {
        public void Configure(EntityTypeBuilder<NotificationDeliveryLocation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();

            builder.HasOne(x => x.Notification).WithMany(x => x.NotificationDeliveryLocations).HasForeignKey(x => x.NotificationId);
            builder.HasOne(x => x.DeliveryLocation).WithMany(x => x.NotificationDeliveryLocations).HasForeignKey(x => x.DeliveryLocationId);
        }
    }
}

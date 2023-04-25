using KeralaMiniMart.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Repository.Configurations
{
    internal class OrderDeliveryStatusConfiguration : IEntityTypeConfiguration<OrderDeliveryStatus>
    {
        public void Configure(EntityTypeBuilder<OrderDeliveryStatus> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.Property(x => x.Status);
        }
    }
}

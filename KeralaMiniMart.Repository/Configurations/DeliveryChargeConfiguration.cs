using KeralaMiniMart.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Repository.Configurations
{
    internal class DeliveryChargeConfiguration : IEntityTypeConfiguration<DeliveryCharge>
    {
        public void Configure(EntityTypeBuilder<DeliveryCharge> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.Property(x => x.Min);
            builder.Property(x => x.Max);
            builder.Property(x => x.Charge);
        }
    }
}

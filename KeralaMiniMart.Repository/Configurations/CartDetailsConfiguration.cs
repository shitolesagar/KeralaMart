using KeralaMiniMart.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Repository.Configurations
{
    internal class CartDetailsConfiguration : IEntityTypeConfiguration<CartDetails>
    {
        public void Configure(EntityTypeBuilder<CartDetails> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();

            builder.HasOne(x => x.ApplicationUser).WithMany(x => x.CartDetails).HasForeignKey(x => x.ApplicationUserId);
            builder.HasOne(x => x.Product).WithMany(x => x.CartDetails).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.Colors).WithMany(x => x.CartDetails).HasForeignKey(x => x.ColorsId);
            builder.HasOne(x => x.SIze).WithMany(x => x.CartDetails).HasForeignKey(x => x.SizeId);
        }
    }
}

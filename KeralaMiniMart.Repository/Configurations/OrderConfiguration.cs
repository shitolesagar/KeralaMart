using KeralaMiniMart.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeralaMiniMart.Repository.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();

            builder.HasOne(x => x.ApplicationUser).WithMany(x => x.Orders).HasForeignKey(x => x.ApplicationUserId);
            builder.HasOne(x => x.UserAddress).WithMany(x => x.Orders).HasForeignKey(x => x.UserAddressId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.PaymentStatus).WithMany(x => x.Orders).HasForeignKey(x => x.PaymentStatusId);
            builder.HasOne(x => x.DeliveryStatus).WithMany(x => x.Orders).HasForeignKey(x => x.DeliveryStatusId);
        }
    }
}

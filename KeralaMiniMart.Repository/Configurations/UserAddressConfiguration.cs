using KeralaMiniMart.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeralaMiniMart.Repository.Configurations
{
    internal class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
    {
        public void Configure(EntityTypeBuilder<UserAddress> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();

            builder.HasOne(x => x.ApplicationUser).WithMany(x => x.UserAddresses).HasForeignKey(x => x.ApplicationUserId);
            builder.HasOne(x => x.DeliveryLocation).WithMany(x => x.UserAddresses).HasForeignKey(x => x.DeliveryLocationId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}

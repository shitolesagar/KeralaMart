using KeralaMiniMart.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeralaMiniMart.Repository.Configurations
{
    internal class DeliveryLocationConfiguration : IEntityTypeConfiguration<DeliveryLocation>
    {
        public void Configure(EntityTypeBuilder<DeliveryLocation> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
        }
    }
}

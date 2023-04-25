using KeralaMiniMart.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeralaMiniMart.Repository.Configurations
{
    internal class ProductSizeConfiguration : IEntityTypeConfiguration<ProductSize>
    {
        public void Configure(EntityTypeBuilder<ProductSize> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();

            builder.HasOne(x => x.Size).WithMany(x => x.ProductSizes).HasForeignKey(x => x.SizeId);
            builder.HasOne(x => x.Product).WithMany(x => x.ProductSizes).HasForeignKey(x => x.ProductId);
        }
    }
}

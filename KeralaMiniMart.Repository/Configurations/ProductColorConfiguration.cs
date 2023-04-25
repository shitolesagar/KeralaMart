using KeralaMiniMart.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeralaMiniMart.Repository.Configurations
{
    internal class ProductColorConfiguration : IEntityTypeConfiguration<ProductColor>
    {
        public void Configure(EntityTypeBuilder<ProductColor> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();

            builder.HasOne(x => x.Product).WithMany(x => x.ProductColors).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.Colors).WithMany(x => x.ProductColors).HasForeignKey(x => x.ColorsId);
        }
    }
}

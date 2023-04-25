using KeralaMiniMart.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeralaMiniMart.Repository.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();

            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.Unit).WithMany(x => x.Products).HasForeignKey(x => x.UnitId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.Subcategory).WithMany(x => x.Products).HasForeignKey(x => x.SubcategoryId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}

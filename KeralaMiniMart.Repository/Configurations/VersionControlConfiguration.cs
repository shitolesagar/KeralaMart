using KeralaMiniMart.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeralaMiniMart.Repository.Configurations
{
    internal class VersionControlConfiguration : IEntityTypeConfiguration<VersionControl>
    {
        public void Configure(EntityTypeBuilder<VersionControl> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
        }
    }
}

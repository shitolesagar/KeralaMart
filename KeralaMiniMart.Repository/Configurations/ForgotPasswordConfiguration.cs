using KeralaMiniMart.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeralaMiniMart.Repository.Configurations
{
    internal class ForgotPasswordConfiguration : IEntityTypeConfiguration<ForgotPassword>
    {
        public void Configure(EntityTypeBuilder<ForgotPassword> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();

            builder.HasOne(x => x.ApplicationUser).WithMany(x => x.ForgotPasswords).HasForeignKey(x => x.ApplicationUserId);
        }
    }
}

using KeralaMiniMart.Entities.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KeralaMiniMart.Repository.Configurations
{
    internal class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.Property(x => x.NotificationType).HasMaxLength(6);

            builder.HasOne(x => x.ApplicationUser).WithMany(x => x.Notifications).HasForeignKey(x => x.ApplicationUserId);
            builder.HasOne(x => x.Category).WithMany(x => x.Notifications).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}

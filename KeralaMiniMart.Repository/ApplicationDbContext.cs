using KeralaMiniMart.Entities.Database;
using KeralaMiniMart.Repository.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KeralaMiniMart.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<ActivityLog> ActivityLog { get; set; }
        public DbSet<AppTheme> AppTheme { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<CartDetails> CartDetail { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Colors> Color { get; set; }
        public DbSet<ExternalLogin> ExternalLogin { get; set; }
        public DbSet<Subcategory> Subcategory { get; set; }
        public DbSet<ForgotPassword> ForgotPassword { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductImages> ProductImage { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<SmtpMail> SmtpMail { get; set; }
        public DbSet<UserAddress> UserAddress { get; set; }
        public DbSet<VersionControl> VersionControl { get; set; }
        public DbSet<DeliveryCharge> DeliveryCharge { get; set; }
        public DbSet<OrderPaymentStatus> OrderPaymentStatu { get; set; }
        public DbSet<OrderDeliveryStatus> OrderDeliveryStatu { get; set; }
        public DbSet<UserDeviceInfo> UserDeviceInfo { get; set; }
        public DbSet<Unit> Unit { get; set; }
        public DbSet<DeliveryLocation> DeliveryLocation { get; set; }
        public DbSet<NotificationDeliveryLocation> NotificationDeliveryLocations { get; set; }
        public DbSet<ConfigurationData> ConfigurationData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new ActivityLogConfiguration());
            modelBuilder.ApplyConfiguration(new AppThemeConfiguration());
            modelBuilder.ApplyConfiguration(new BannerConfiguration());
            modelBuilder.ApplyConfiguration(new CartDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ColorsConfiguration());
            modelBuilder.ApplyConfiguration(new ExternalLoginConfiguration());
            modelBuilder.ApplyConfiguration(new SubcategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ForgotPasswordConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailsConfiguration());
            modelBuilder.ApplyConfiguration(new VersionControlConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductColorConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImagesConfiguration());
            modelBuilder.ApplyConfiguration(new ProductSizeConfiguration());
            modelBuilder.ApplyConfiguration(new SizeConfiguration());
            modelBuilder.ApplyConfiguration(new SmtpMailConfiguration());
            modelBuilder.ApplyConfiguration(new UserAddressConfiguration());
            modelBuilder.ApplyConfiguration(new DeliveryChargeConfiguration());
            modelBuilder.ApplyConfiguration(new OrderPaymentStatusConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDeliveryStatusConfiguration());
            modelBuilder.ApplyConfiguration(new UserDeviceInfoConfiguration());
            modelBuilder.ApplyConfiguration(new UnitsConfiguration());
            modelBuilder.ApplyConfiguration(new DeliveryLocationConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationDeliveryLocationConfiguration());
            modelBuilder.ApplyConfiguration(new ConfigurationDataConfiguration());
        }
    }
}

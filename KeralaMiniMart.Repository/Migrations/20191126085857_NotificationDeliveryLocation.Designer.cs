﻿// <auto-generated />
using System;
using KeralaMiniMart.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KeralaMiniMart.Repository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20191126085857_NotificationDeliveryLocation")]
    partial class NotificationDeliveryLocation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.ActivityLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApplicationUserId");

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<string>("Task");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("ActivityLog");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.AppTheme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppLogo");

                    b.Property<string>("AppName");

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<string>("CurrencySymbols");

                    b.Property<string>("PrimaryColor");

                    b.Property<string>("PrimaryTextColor");

                    b.Property<string>("SecondaryTextColor");

                    b.Property<string>("SecondryColor");

                    b.Property<string>("StatusBarColor");

                    b.Property<string>("TertiaryColor");

                    b.HasKey("Id");

                    b.ToTable("AppTheme");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<string>("Email");

                    b.Property<bool>("IsOTPVerified");

                    b.Property<string>("MobileNumber");

                    b.Property<string>("Name");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("ProfilePicture");

                    b.Property<int>("RoleId");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("ApplicationUser");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.Banner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<DateTime?>("ExpiryDate");

                    b.Property<string>("Image");

                    b.Property<DateTime>("ModifiedDateTime");

                    b.Property<DateTime?>("StartDate");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Banner");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.CartDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApplicationUserId");

                    b.Property<int?>("ColorsId");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<int?>("SizeId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("ColorsId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SizeId");

                    b.ToTable("CartDetail");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<string>("Image");

                    b.Property<DateTime>("ModifiedDateTime");

                    b.Property<string>("Name");

                    b.Property<string>("ShortDescription");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.Colors", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<string>("HashCode");

                    b.Property<DateTime>("ModifiedDateTime");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Color");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.DeliveryCharge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Charge");

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<int>("Max");

                    b.Property<int>("Min");

                    b.Property<DateTime>("ModifiedDateTime");

                    b.HasKey("Id");

                    b.ToTable("DeliveryCharge");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.DeliveryLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Area");

                    b.Property<string>("Day");

                    b.Property<string>("Pincode");

                    b.Property<string>("Time");

                    b.HasKey("Id");

                    b.ToTable("DeliveryLocation");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.ExternalLogin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LoginProvider");

                    b.Property<int>("ProviderKey");

                    b.HasKey("Id");

                    b.ToTable("ExternalLogin");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.ForgotPassword", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApplicationUserId");

                    b.Property<DateTime>("ExpireDate");

                    b.Property<bool>("IsUsed");

                    b.Property<string>("OTP");

                    b.Property<string>("SMSResponse");

                    b.Property<string>("Token");

                    b.Property<string>("VerificationCode");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("ForgotPassword");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApplicationUserId");

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Message");

                    b.Property<string>("NotificationType")
                        .HasMaxLength(6);

                    b.Property<string>("Platform");

                    b.Property<string>("Redirect");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.NotificationDeliveryLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("DeliveryLocationId");

                    b.Property<int?>("NotificationId");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryLocationId");

                    b.HasIndex("NotificationId");

                    b.ToTable("NotificationDeliveryLocations");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApplicationUserId");

                    b.Property<string>("Comments");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime?>("DeliveredDate");

                    b.Property<string>("DeliveredSmsResponse");

                    b.Property<decimal>("DeliveryCharges");

                    b.Property<int>("DeliveryStatusId");

                    b.Property<decimal>("DiscountPrice");

                    b.Property<DateTime>("EstimatedDeliveryDate");

                    b.Property<decimal>("GSTPrice");

                    b.Property<int>("MRP");

                    b.Property<string>("OrderNumber");

                    b.Property<int>("PaymentStatusId");

                    b.Property<decimal>("SubTotalPrice");

                    b.Property<decimal>("TotalPrice");

                    b.Property<string>("TransactionId");

                    b.Property<int>("UserAddressId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("DeliveryStatusId");

                    b.HasIndex("PaymentStatusId");

                    b.HasIndex("UserAddressId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.OrderDeliveryStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.ToTable("OrderDeliveryStatu");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ColorsId");

                    b.Property<int>("DiscountedPrice");

                    b.Property<int>("OrderId");

                    b.Property<int>("OriginalPrice");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<int?>("SizeId");

                    b.HasKey("Id");

                    b.HasIndex("ColorsId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SizeId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.OrderPaymentStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.ToTable("OrderPaymentStatu");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AvailableQuantity");

                    b.Property<string>("Brand");

                    b.Property<int?>("CategoryId");

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<int>("DeliveryDays");

                    b.Property<string>("Description");

                    b.Property<int>("DiscountPercentage");

                    b.Property<decimal>("DiscountedPrice");

                    b.Property<string>("Height");

                    b.Property<string>("IncludedAccessories");

                    b.Property<bool>("IsAutomateStockMaintainance");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsExclusive");

                    b.Property<bool>("IsPublish");

                    b.Property<string>("Length");

                    b.Property<string>("MaterialType");

                    b.Property<DateTime>("ModifiedDateTime");

                    b.Property<string>("Name");

                    b.Property<decimal?>("OriginalPrice");

                    b.Property<string>("Precautions");

                    b.Property<string>("Quantity");

                    b.Property<string>("StockKeepingUnit");

                    b.Property<int?>("SubcategoryId");

                    b.Property<int?>("UnitId");

                    b.Property<string>("Weight");

                    b.Property<string>("Width");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SubcategoryId");

                    b.HasIndex("UnitId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.ProductColor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ColorsId");

                    b.Property<int>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ColorsId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductColors");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.ProductImages", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Image");

                    b.Property<bool>("IsMain");

                    b.Property<int>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImage");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.ProductSize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductId");

                    b.Property<int>("SizeId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("SizeId");

                    b.ToTable("ProductSizes");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.Size", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ProductSize");

                    b.Property<string>("Unit");

                    b.HasKey("Id");

                    b.ToTable("Size");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.SmtpMail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("DisplayName");

                    b.Property<string>("FromMail");

                    b.Property<string>("Host");

                    b.Property<int>("Port");

                    b.Property<string>("SmtpPassword");

                    b.HasKey("Id");

                    b.ToTable("SmtpMail");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.Subcategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("CreatedDateTime");

                    b.Property<string>("ImagePath");

                    b.Property<DateTime>("ModifiedDateTime");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Subcategory");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.Unit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UnitName");

                    b.HasKey("Id");

                    b.ToTable("Unit");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.UserAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<int>("ApplicationUserId");

                    b.Property<int?>("DeliveryLocationId");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Landmark");

                    b.Property<string>("Locality");

                    b.Property<string>("MobileNumber");

                    b.Property<string>("Pincode");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("DeliveryLocationId");

                    b.ToTable("UserAddress");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.UserDeviceInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ApplicationUserId");

                    b.Property<string>("DeviceId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("UserDeviceInfo");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.VersionControl", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CurrentLiveVersion");

                    b.Property<DateTime>("Date");

                    b.Property<string>("UpdateType");

                    b.Property<float>("VersionNumber");

                    b.HasKey("Id");

                    b.ToTable("VersionControl");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.ActivityLog", b =>
                {
                    b.HasOne("KeralaMiniMart.Entities.Database.ApplicationUser", "ApplicationUser")
                        .WithMany("ActivityLogs")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.ApplicationUser", b =>
                {
                    b.HasOne("KeralaMiniMart.Entities.Database.Role", "Role")
                        .WithMany("ApplicationUsers")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.CartDetails", b =>
                {
                    b.HasOne("KeralaMiniMart.Entities.Database.ApplicationUser", "ApplicationUser")
                        .WithMany("CartDetails")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KeralaMiniMart.Entities.Database.Colors", "Colors")
                        .WithMany("CartDetails")
                        .HasForeignKey("ColorsId");

                    b.HasOne("KeralaMiniMart.Entities.Database.Product", "Product")
                        .WithMany("CartDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KeralaMiniMart.Entities.Database.Size", "SIze")
                        .WithMany("CartDetails")
                        .HasForeignKey("SizeId");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.ForgotPassword", b =>
                {
                    b.HasOne("KeralaMiniMart.Entities.Database.ApplicationUser", "ApplicationUser")
                        .WithMany("ForgotPasswords")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.Notification", b =>
                {
                    b.HasOne("KeralaMiniMart.Entities.Database.ApplicationUser", "ApplicationUser")
                        .WithMany("Notifications")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.NotificationDeliveryLocation", b =>
                {
                    b.HasOne("KeralaMiniMart.Entities.Database.DeliveryLocation", "DeliveryLocation")
                        .WithMany("NotificationDeliveryLocations")
                        .HasForeignKey("DeliveryLocationId");

                    b.HasOne("KeralaMiniMart.Entities.Database.Notification", "Notification")
                        .WithMany("NotificationDeliveryLocations")
                        .HasForeignKey("NotificationId");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.Order", b =>
                {
                    b.HasOne("KeralaMiniMart.Entities.Database.ApplicationUser", "ApplicationUser")
                        .WithMany("Orders")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KeralaMiniMart.Entities.Database.OrderDeliveryStatus", "DeliveryStatus")
                        .WithMany("Orders")
                        .HasForeignKey("DeliveryStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KeralaMiniMart.Entities.Database.OrderPaymentStatus", "PaymentStatus")
                        .WithMany("Orders")
                        .HasForeignKey("PaymentStatusId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KeralaMiniMart.Entities.Database.UserAddress", "UserAddress")
                        .WithMany("Orders")
                        .HasForeignKey("UserAddressId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.OrderDetail", b =>
                {
                    b.HasOne("KeralaMiniMart.Entities.Database.Colors", "Colors")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ColorsId");

                    b.HasOne("KeralaMiniMart.Entities.Database.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KeralaMiniMart.Entities.Database.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KeralaMiniMart.Entities.Database.Size", "Size")
                        .WithMany("OrderDetails")
                        .HasForeignKey("SizeId");
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.Product", b =>
                {
                    b.HasOne("KeralaMiniMart.Entities.Database.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("KeralaMiniMart.Entities.Database.Subcategory", "Subcategory")
                        .WithMany("Products")
                        .HasForeignKey("SubcategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("KeralaMiniMart.Entities.Database.Unit", "Unit")
                        .WithMany("Products")
                        .HasForeignKey("UnitId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.ProductColor", b =>
                {
                    b.HasOne("KeralaMiniMart.Entities.Database.Colors", "Colors")
                        .WithMany("ProductColors")
                        .HasForeignKey("ColorsId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KeralaMiniMart.Entities.Database.Product", "Product")
                        .WithMany("ProductColors")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.ProductImages", b =>
                {
                    b.HasOne("KeralaMiniMart.Entities.Database.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.ProductSize", b =>
                {
                    b.HasOne("KeralaMiniMart.Entities.Database.Product", "Product")
                        .WithMany("ProductSizes")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KeralaMiniMart.Entities.Database.Size", "Size")
                        .WithMany("ProductSizes")
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.Subcategory", b =>
                {
                    b.HasOne("KeralaMiniMart.Entities.Database.Category", "Category")
                        .WithMany("Filters")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.UserAddress", b =>
                {
                    b.HasOne("KeralaMiniMart.Entities.Database.ApplicationUser", "ApplicationUser")
                        .WithMany("UserAddresses")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KeralaMiniMart.Entities.Database.DeliveryLocation", "DeliveryLocation")
                        .WithMany("UserAddresses")
                        .HasForeignKey("DeliveryLocationId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("KeralaMiniMart.Entities.Database.UserDeviceInfo", b =>
                {
                    b.HasOne("KeralaMiniMart.Entities.Database.ApplicationUser", "ApplicationUser")
                        .WithMany("UserDeviceInfos")
                        .HasForeignKey("ApplicationUserId");
                });
#pragma warning restore 612, 618
        }
    }
}

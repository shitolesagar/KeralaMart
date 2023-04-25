using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using KeralaMiniMart.Abstraction;
using KeralaMiniMart.Abstraction.Repositories;
using KeralaMiniMart.Abstraction.Service;
using KeralaMiniMart.Entities;
using KeralaMiniMart.Entities.Constant;
using KeralaMiniMart.Repository;
using KeralaMiniMart.Repository.Repositories;
using KeralaMiniMart.Service;
using KeralaMiniMart.Service.ExtensionMethods;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KeralaMiniMart.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Services
            services.AddScoped<IFileServices, FileServices>();
            services.AddScoped<IWebCategoryService, WebCategoryService>();
            services.AddScoped<ISubcategoryService, SubcategoryService>();
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFCMPushNotificationService, FCMPushNotificationService>();
            services.AddScoped<ISmsService, SmsService>();
            services.AddScoped<ICheckoutService, CheckoutService>();

            #endregion

            #region Repostiory
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISubcategoryRepository, SubcategoryRepository>();
            services.AddScoped<IBannerRepository, BannerRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IForgotPasswordRepository, ForgotPasswordRepository>();
            services.AddScoped<IOrderDetailsRepository, OrderDetailsRepository>();
            services.AddScoped<IUserAddressRepository, UserAddressRepository>();
            services.AddScoped<IProductImagesRepository, ProductImagesRepository>();
            services.AddScoped<ISmtpMailRepository, SmtpMailRepository>();
            services.AddScoped<IOrderPaymentStatusRepository, OrderPaymentStatusRepository>();
            services.AddScoped<IOrderDeliveryStatusRepository, OrderDeliveryStatusRepository>();
            services.AddScoped<IColorsRepository, ColorsRepository>();
            services.AddScoped<ISizeRepository, SizeRepository>();
            services.AddScoped<IProductSizeRepository, ProductSizeRepository>();
            services.AddScoped<IProductColorRepository, ProductColorRepository>();
            services.AddScoped<IDeliveryLocationRepository, DeliveryLocationRepository>();
            services.AddScoped<IDelivery_chargeRepository, Delivery_chargeRepository>();
            services.AddScoped<IUserDeviceInfoRepository, UserDeviceInfoRepository>();
            services.AddScoped<ICartDetailsRepository, CartDetailsRepository>();
            services.AddScoped<IVersionControlRepository, VersionControlRepository>();
            services.AddScoped<IAppThemeRepository, AppThemeRepository>();
            services.AddScoped<IUnitRepository, UnitRepository>();
            services.AddScoped<INotificationDeliveryLocationRepository, NotificationDeliveryLocationRepository>();
            services.AddScoped<IConfigurationDataRepository, ConfigurationDataRepository>();
            #endregion
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseMySql(Configuration.GetConnectionString("defaultConnection")));

            services.Configure<AppSettings>(Configuration.GetSection("ApplicationSettings"));

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-IN");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-US"), new CultureInfo("en-IN") };
            });


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/account/index";
                        options.Cookie.Name = EnvironmentConstants.CookiesName;
                    });

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                 .RequireAuthenticatedUser()
                                 .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Account/Error");
            }
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=account}/{action=index}/{id?}");
            });
        }
    }
}

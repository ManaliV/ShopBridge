using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using ShopBridge.Core.Interface.Services.Catalog;
using ShopBridge.Core.Interface.Services.Messages;
using ShopBridge.Core.Interface.Services.Sale;
using ShopBridge.Core.Interface.Services.Statistics;
using ShopBridge.Core.Interface.Services.User;
using ShopBridge.Infrastructure;
using ShopBridge.Infrastructure.EFModels;
using ShopBridge.Infrastructure.EFRepository;
using ShopBridge.Infrastructure.Services.Catalog;
using ShopBridge.Infrastructure.Services.Messages;
using ShopBridge.Infrastructure.Services.Sale;
using ShopBridge.Infrastructure.Services.Statistics;
using ShopBridge.Infrastructure.Services.User;
using ShopBridge.Web.Areas.Admin.Helpers;
using ShopBridge.Web.Helpers;
using ShopBridge.Web.Models;

namespace ShopBridge.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public MapperConfiguration MapperConfiguration { get; set; }
        public IConfigurationRoot ConfigurationBuild { get; set; }



        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });

            services.Configure<AdminAccount>(
                Configuration.GetSection("AdminAccount"));

            services.Configure<UserAccount>(
                Configuration.GetSection("UserAccount"));
            
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(1);
            });

            services.AddMvc();

            // Add application services.
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IBillingAddressService, BillingAddressService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IImageManagerService, ImageManagerService>();
            services.AddTransient<IManufacturerService, ManufacturerService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<ISpecificationService, SpecificationService>();

            services.AddTransient<IOrderCountService, OrderCountService>();
            services.AddTransient<IVisitorCountService, VisitorCountService>();

            services.AddTransient<IContactUsService, ContactUsService>();

            services.AddSingleton(sp => MapperConfiguration.CreateMapper());
            services.AddScoped<ViewHelper>();
            services.AddScoped<DataHelper>();
          //  services.AddSingleton<IFileProvider>(HostingEnvironment.ContentRootFileProvider);



            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
               // app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                 name: "Admin",
                 areaName: "Admin",
                 pattern: "Admin/{controller=Dashboard}/{action=Index}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                               
            }

            builder.AddEnvironmentVariables();
            ConfigurationBuild = builder.Build();
            //HostingEnvironment = env;

            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfileConfiguration());
            });

            // app.UseSession();


            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "areaRoute",
            //        template: "{area:exists}/{controller}/{action}/{id?}",
            //        defaults: new { controller = "Dashboard", action = "Index" });

            //    routes.MapRoute(
            //        name: "productInfo",
            //        template: "Product/{seo}",
            //        defaults: new { controller = "Home", action = "ProductInfo" });

            //    routes.MapRoute(
            //        name: "category",
            //        template: "Category/{category}",
            //        defaults: new { controller = "Home", action = "ProductCategory" });

            //    routes.MapRoute(
            //        name: "manufacturer",
            //        template: "Manufacturer/{manufacturer}",
            //        defaults: new { controller = "Home", action = "ProductManufacturer" });

            //    routes.MapRoute(
            //        name: "productSearch",
            //        template: "search/{name?}",
            //        defaults: new { controller = "Home", action = "ProductSearch" });

            //    routes.MapRoute(
            //        name: "create review",
            //        template: "CreateReview/{id}",
            //        defaults: new { controller = "Home", action = "CreateReview" });

            //    routes.MapRoute(
            //        name: "default",
            //        template: "{controller=Home}/{action=Index}/{id?}");
            //});

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                
                // apply migration
                SampleDataProvider.ApplyMigration(serviceScope.ServiceProvider);

                //// seed default data
                SampleDataProvider.Seed(serviceScope.ServiceProvider, ConfigurationBuild);

                // Seed the database.
            }
            
        }
    }
}

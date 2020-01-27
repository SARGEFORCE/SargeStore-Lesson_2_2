using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SargeStore.Interfaces.Api;
using SargeStore.Interfaces.Services;
using SargeStore.Services.FProduct;
using SargeStoreDomain.Entities.Identity;
using SargeStore.Clients.Values;
using System;
using SargeStore.Clients.Employees;
using SargeStore.Clients.Products;
using SargeStore.Clients.Orders;
using SargeStore.Clients.Identity;
using SargeStore.Logger;
using Microsoft.Extensions.Logging;

namespace SargeStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration Config) => Configuration = Config;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IEmployeesData, EmployeesClient>();
            services.AddScoped<IProductData, ProductsClient>();
            services.AddScoped<IOrderService, OrdersClient>();
            services.AddScoped<ICartService, CookieCartService>();
            services.AddScoped<ICartService, CookieCartService>();
            services.AddTransient<IValuesService, ValuesClient>();

            #region Custom Implementation identity storage

            services.AddTransient<IUserStore<User>, UsersClient>();
            services.AddTransient<IUserRoleStore<User>, UsersClient>();
            services.AddTransient<IUserClaimStore<User>, UsersClient>();
            services.AddTransient<IUserPasswordStore<User>, UsersClient>();
            services.AddTransient<IUserEmailStore<User>, UsersClient>();
            services.AddTransient<IUserPhoneNumberStore<User>, UsersClient>();
            services.AddTransient<IUserTwoFactorStore<User>, UsersClient>();
            services.AddTransient<IUserLoginStore<User>, UsersClient>();
            services.AddTransient<IUserLockoutStore<User>, UsersClient>();

            services.AddTransient<IRoleStore<Role>, RolesClient>();

            #endregion

            services.AddIdentity<User, Role>()
                //.AddEntityFrameworkStores<SargeStoreDB>()
                .AddDefaultTokenProviders();
            

            services.ConfigureApplicationCookie(opt =>
            {
                var polgoda = TimeSpan.FromDays(182);
                opt.Cookie.Name = "SargeStore-Identity";
                opt.Cookie.HttpOnly = true;
                opt.Cookie.Expiration = polgoda;

                opt.LoginPath = "/Account/Login";
                opt.LogoutPath = "/Account/Logout";
                opt.AccessDeniedPath = "/Account/AccessDenided";

                opt.SlidingExpiration = true;
            });

            services.AddSession();
            services.AddMvc();

        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory log)
        {
            log.AddLog4Net();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes => 
            {
                    routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                    routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            }); 
            //app.UseMvcWithDefaultRoute(); //или можно так написать, что тоже самое
        }
    }
}

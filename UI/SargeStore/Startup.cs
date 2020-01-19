using DataAccessLayer.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SargeStore.Interfaces.Api;
using SargeStore.Interfaces.Services;
using SargeStore.Services.Database;
using SargeStore.Services.FProduct;
using SargeStoreDomain.Entities.Identity;
using SargeStore.Clients.Values;
using System;

namespace SargeStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration Config) => Configuration = Config;

        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddScoped<ICartService, CookieCartService>();
            services.AddTransient<IValuesService, ValuesClient>();
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<SargeStoreDB>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 3;
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredUniqueChars = 3;

                opt.Lockout.AllowedForNewUsers = true;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);

                //opt.User.AllowedUserNameCharacters = "abcdef...";
                opt.User.RequireUniqueEmail = false; //Грабли - на этапе отладки при попытке рег двух юзеров без мыл                
            });

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


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SargeStoreContexInitializer db)
        {
            db.InitializeAsync().Wait();

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

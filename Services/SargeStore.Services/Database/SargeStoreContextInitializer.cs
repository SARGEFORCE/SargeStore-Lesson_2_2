﻿using DataAccessLayer.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SargeStoreDomain.Entities.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SargeStore.Services.Database
{
    public class SargeStoreContextInitializer
    {
        private readonly SargeStoreDB _db;
        private readonly UserManager<User> _UserManager;
        private readonly RoleManager<Role> _RoleManager;
        private readonly ILogger<SargeStoreContextInitializer> _Logger;

        public SargeStoreContextInitializer(
            SargeStoreDB db, 
            UserManager<User> UserManager, 
            RoleManager<Role> RoleManager,
            ILogger<SargeStoreContextInitializer> Logger)
        {
            _db = db;
            _UserManager = UserManager;
            _RoleManager = RoleManager;
            _Logger = Logger;
        }

        public async Task InitializeAsync()
        {
            var db = _db.Database;
            //if(await _db.Database.EnsureDeletedAsync())
            //{
            //    //БД существовала и была успешно удалена
            //}

            //await _db.Database.EnsureCreatedAsync();

            await db.MigrateAsync(); //Автоматическое создание и миграция до последней версии

            await IdentityInitializeAsync();

            if (await _db.Products.AnyAsync()) return;

            using (var transaction = await db.BeginTransactionAsync())
            {
                await _db.Sections.AddRangeAsync(TestData.Sections);

                await db.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Sections] ON");
                await _db.SaveChangesAsync();
                await db.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Sections] OFF");
                transaction.Commit();
            }

            using (var transaction = await db.BeginTransactionAsync())
            {
                await _db.Brands.AddRangeAsync(TestData.Brands);

                await db.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Brands] ON");
                await _db.SaveChangesAsync();
                await db.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Brands] OFF");
                transaction.Commit();
            }

            using (var transaction = await db.BeginTransactionAsync())
            {
                await _db.Products.AddRangeAsync(TestData.Products);

                await db.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Products] ON");
                await _db.SaveChangesAsync();
                await db.ExecuteSqlCommandAsync("SET IDENTITY_INSERT [dbo].[Products] OFF");
                transaction.Commit();
            }
        }
        private async Task IdentityInitializeAsync()
        {
            if (!await _RoleManager.RoleExistsAsync(Role.Administrator))
                await _RoleManager.CreateAsync(new Role { Name = Role.Administrator });

            if (!await _RoleManager.RoleExistsAsync(Role.User))
                await _RoleManager.CreateAsync(new Role { Name = Role.User });

            if (await _UserManager.FindByNameAsync(User.Administrator) is null)
            {
                var admin = new User
                {
                    UserName = User.Administrator
                };
                var creation_result = await _UserManager.CreateAsync(admin, User.AdminPasswordDefault);

                if (creation_result.Succeeded)
                    await _UserManager.AddToRoleAsync(admin, Role.Administrator);
                else
                {
                    var errors = string.Join(", ", creation_result.Errors.Select(e => e.Description));
                    _Logger.LogError("Ошибка при создании пользователя Админимтратор в БД {0}", errors);
                    throw new InvalidOperationException($"Ошибка при создании админа в БД {errors}");
                }
            }
        }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SargeStoreDomain.Entities;
using SargeStoreDomain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Context
{
    public class SargeStoreDB : IdentityDbContext<User, Role, string>
    {
        public DbSet<Brand >Brands {get; set; }
        public DbSet<Section> Sections{ get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public SargeStoreDB(DbContextOptions<SargeStoreDB> options) : base(options) { }
    }
}

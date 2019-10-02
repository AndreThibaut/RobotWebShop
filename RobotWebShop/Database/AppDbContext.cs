using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RobotWebShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RobotWebShop.Database
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Robot> Robots { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<OrderStock> OrderStocks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<StockOnHold> StockOnHolds { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<OrderStock>()
                .HasKey(x => new { x.StockID, x.OrderID });
        }
    }
}

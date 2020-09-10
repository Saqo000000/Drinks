using Drinks.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Drinks.Data.Data
{
    public class DrinkDbContext : DbContext
    {
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<DrinkCategory> Categories { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<DrinkMenu> DrinkMenus { get; set; }
        public DrinkDbContext(DbContextOptions<DrinkDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DrinkMenu>().HasKey(d => new { d.DrinkID, d.MenuID });
        }
    }
}

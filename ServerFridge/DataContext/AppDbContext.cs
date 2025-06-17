using Microsoft.EntityFrameworkCore;
using ServerFridge.Configuration;
using ServerFridge.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServerFridge.DataContext
{
    public class AppDbContext:DbContext
    { 
       public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FridgeModelConfiguration());
            modelBuilder.ApplyConfiguration(new FridgeConfiguration());
            modelBuilder.ApplyConfiguration(new FridgeProductsConfiguration());
            modelBuilder.ApplyConfiguration(new ProductsConfiguration());   
        }

        public DbSet<Fridge> Fridges { get; set; }  
        public DbSet<FridgeModel> FridgeModels { get; set; }
        public DbSet<FridgeProducts> FridgeProducts { get; set; }
        public DbSet<Products> Products { get; set; }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        //{
        //    optionBuilder.UseSqlServer(@"Data Source=YARIK-BOOK; Initial Catalog=FridgeSystem;Trusted_Connection= true");
        //}


    }
}

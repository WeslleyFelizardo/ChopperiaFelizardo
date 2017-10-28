using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChopperiaFelizardo.Models
{
    public class ChopperiaContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Situation> Situations { get; set; }
        public DbSet<FormPayment> FormsPayments { get; set; }
        public DbSet<TypeTransation> Types { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Box> Boxs { get; set; }
        public DbSet<Transation> Transations { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderPayment> OrdersPayments { get; set; }
        public DbSet<Item> Items { get; set; }
        //public DbSet<TablePrice> TablePrices { get; set; }

        public ChopperiaContext(DbContextOptions<ChopperiaContext> options) :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

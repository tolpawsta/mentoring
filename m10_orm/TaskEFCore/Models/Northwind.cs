using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using TaskEFCore.Configurations;

namespace TaskEFCore.Models
{
    public class Northwind : DbContext
    {
        private string _connectionString;
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        public Northwind(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (options!=null)
            {
                options.UseSqlServer(_connectionString);
            }            
            base.OnConfiguring(options);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration())
            .ApplyConfiguration(new OrderConfiguration())
            .ApplyConfiguration(new OrderDetailConfiguration())
            .ApplyConfiguration(new CustomerConfiguration())
            .ApplyConfiguration(new CategoryConfiguration());
        }
    }
}

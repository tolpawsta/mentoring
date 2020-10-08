using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Linq;
using TaskEFCore.Configurations;

namespace TaskEFCore.Models
{
    public class Northwind : DbContext
    {
        private const string CONNECTIONNAME = "NorthwindDB";
        private readonly string _connectionString;
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        public Northwind()
        {
           // _connectionString = ConfigurationManager.ConnectionStrings[CONNECTIONNAME].ConnectionString;
        }
        public Northwind(DbContextOptions<Northwind> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: See DbContextOptionsBuilder
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer(_connectionString);
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Northwind;Trusted_Connection=true");
            }

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration())
            .ApplyConfiguration(new OrderConfiguration())
            .ApplyConfiguration(new OrderDetailConfiguration())
            .ApplyConfiguration(new CustomerConfiguration())
            .ApplyConfiguration(new CategoryConfiguration())
            .ApplyConfiguration(new RegionConfiguration())
            .ApplyConfiguration(new EmployeeConfiguration())
            .ApplyConfiguration(new EmployeeCreditCardConfiguration());
        }
    }
}

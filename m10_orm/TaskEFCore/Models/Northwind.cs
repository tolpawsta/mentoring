using Microsoft.EntityFrameworkCore;
using TaskEFCore.Configurations;

namespace TaskEFCore.Models
{
    public class Northwind : DbContext
    {
        private const string CONNECTIONSTRING= "Server=(localdb)\\MSSQLLocalDB;Database=Northwind;Trusted_Connection=true";
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        public Northwind()
        {
           
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
                optionsBuilder.UseSqlServer(CONNECTIONSTRING);
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
            .ApplyConfiguration(new EmployeeCreditCardConfiguration())
            .ApplyConfiguration(new TerritoryConfiguration());
        }
    }
}

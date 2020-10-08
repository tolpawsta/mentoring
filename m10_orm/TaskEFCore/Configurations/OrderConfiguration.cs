using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskEFCore.Models;

namespace TaskEFCore.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id)
                .HasName("OrderID");

            builder.HasIndex(e => e.CustomerId)
                .HasName("CustomerOrders");

            builder.HasIndex(e => e.EmployeeId)
                .HasName("EmployeesOrders");

            builder.HasIndex(e => e.OrderDate)
                .HasName("OrderDate");

            builder.HasIndex(e => e.ShippedDate)
                .HasName("ShippedDate");

            builder.Property(e => e.Id)
                .HasColumnName("OrderID");

            builder.Property(e => e.OrderDate)
                .HasColumnType("datetime");

            builder.Property(e => e.ShippedDate)
                .HasColumnType("datetime");

            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(o => o.CustomerId)
                .HasConstraintName("FK_Orders_Customers");
            
            builder.HasOne(o => o.Employee)
                .WithMany(e => e.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(o => o.EmployeeId)
                .HasConstraintName("FK_Orders_Employees");
        }
    }
}
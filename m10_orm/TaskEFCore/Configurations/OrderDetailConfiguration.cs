using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskEFCore.Models;

namespace TaskEFCore.Configurations
{
    internal class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("Order Details");
            
            builder.HasKey(o => new { o.OrderId, o.ProductId })
                .IsClustered(false);

            builder.HasIndex(o => o.OrderId)
                .HasName("OrderID");           

            builder.HasIndex(o => o.ProductId)
                .HasName("ProductID");            

            builder.Property(o => o.OrderId)
                .HasColumnName("OrderID");

            builder.Property(o => o.ProductId)
                .HasColumnName("ProductID");

            builder.Property(o => o.UnitPrice)
                .IsRequired()
                .HasColumnType("money");

            builder.HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                //TODO: DeleteBehavior learn
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Details_Orders");

            builder.HasOne(o => o.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Details_Products");
        }
    }
}
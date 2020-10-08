using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskEFCore.Models;

namespace TaskEFCore.Configurations

{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductID)
                .HasName("ProductID");

            builder.HasIndex(p => p.CategoryId)
                .HasName("CatagoryID");

            builder.HasIndex(p => p.Name)
                .HasName("ProductName");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnName("ProductName");

            builder.Property(p => p.Coast)
                .HasColumnName("UnitPrice")
                .HasColumnType("money");

            builder.HasOne(p=>p.Category)
                .WithMany(c=>c.Products)
                .HasForeignKey(p=>p.CategoryId)
                .HasConstraintName("FK_Products_Categories");              
        }
    }
}
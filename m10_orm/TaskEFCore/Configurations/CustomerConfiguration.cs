using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskEFCore.Models;

namespace TaskEFCore.Configurations
{
    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.CustomerID)
                .HasName("CustomerID");

            builder.HasIndex(c => c.CompanyName)
                .HasName("CompanyName");

            builder.Property(c => c.CustomerID)
                .HasColumnName("CustomerID")
                .HasMaxLength(5);
            //TODO: .IsFixedLength();
            builder.Property(c => c.CompanyName)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(c => c.Name)
                .HasColumnName("ContactName")
                .HasMaxLength(30);

            builder.Property(c => c.Phone)
                .HasMaxLength(24);

            builder.Property(c => c.FoundationDate)
                .HasColumnType("datetime");           
        }
    }
}
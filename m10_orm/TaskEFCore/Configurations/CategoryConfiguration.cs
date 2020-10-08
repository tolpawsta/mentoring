using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskEFCore.Models;

namespace TaskEFCore.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryID)
                .HasName("CategoryID");

            builder.Property(c => c.CategoryID)
                .HasColumnName("CategoryID");

            builder.HasIndex(c => c.Name)
                .HasName("CategoryName");

            builder.Property(c => c.Name)
                .HasColumnName("CategoryName")
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(c => c.Description)
                .HasColumnType("ntext");

        }
    }
}
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

            builder.HasData(
                new Category()
                {
                    CategoryID=1,
                    Name = "Beverages",
                    Description = "Soft drinks, coffees, teas, beers, and ales"
                },
                new Category()
                {
                    CategoryID = 2,
                    Name = "Condiments",
                    Description = "Sweet and savory sauces, relishes, spreads, and seasonings"
                },
                new Category()
                {
                    CategoryID = 3,
                    Name = "Confections",
                    Description = "Desserts, candies, and sweet breads"
                },
                new Category()
                {
                    CategoryID = 4,
                    Name = "Dairy Products",
                    Description = "Cheeses"
                },
                new Category()
                {
                    CategoryID = 5,
                    Name = "Grains/Cereals",
                    Description = "Breads, crackers, pasta, and cereal"
                },
                new Category()
                {
                    CategoryID = 6,
                    Name = "Meat/Poultry",
                    Description = "Prepared meats"
                },
                new Category()
                {
                    CategoryID = 7,
                    Name = "Produce",
                    Description = "Dried fruit and bean curd"
                },
                new Category()
                {
                    CategoryID = 8,
                    Name = "Seafood",
                    Description = "Seaweed and fish"
                }
            );
        }
    }
}
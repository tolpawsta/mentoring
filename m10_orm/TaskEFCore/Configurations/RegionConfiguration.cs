using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskEFCore.Models;

namespace TaskEFCore.Configurations
{
    class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable("Regions");

            builder.HasKey(r => r.Id)
                .HasName("RedionID");

            builder.Property(r => r.Id)
                .HasColumnName("RegionID");

            builder.Property(r => r.RegionDescription)
                .HasColumnName("RegionDescription")
                .IsRequired()
                .HasMaxLength(50);
              
            builder.HasData(
                new Region()
                {
                    Id=1,
                    RegionDescription = "Eastern"
                },
                new Region()
                {
                    Id = 2,
                    RegionDescription = "Western"
                },
                new Region()
                {
                    Id = 3,
                    RegionDescription = "Northern"
                },
                new Region()
                {
                    Id = 4,
                    RegionDescription = "Southern"
                }
                );
        }
    }
}

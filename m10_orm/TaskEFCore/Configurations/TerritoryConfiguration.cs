using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TaskEFCore.Models;

namespace TaskEFCore.Configurations
{
    class TerritoryConfiguration : IEntityTypeConfiguration<Territory>
    {
        public void Configure(EntityTypeBuilder<Territory> builder)
        {
            builder.ToTable("Territories");

            builder.HasKey(t => t.Id)
                .HasName("TerritoryID");

            builder.Property(t => t.Id)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(t => t.TerritoryDescription)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(t => t.Region)
                .WithMany(r => r.Territories)
                .HasForeignKey(t => t.RegionId)
                .HasConstraintName("FK_Terriroties_Region");
            
            builder.HasData(
                new Territory()
                {
                    Id= "01581",
                    TerritoryDescription= "Westboro",
                    RegionId=1
                },
                new Territory()
                {
                    Id= "01833",
                    TerritoryDescription= "Georgetow",
                    RegionId=1
                },
                new Territory()
                {
                    Id= "10019",
                    TerritoryDescription= "New York",
                    RegionId=1
                },
                new Territory()
                {
                    Id= "80202",
                    TerritoryDescription= "Denver",
                    RegionId=2
                },
                new Territory()
                {
                    Id= "94105",
                    TerritoryDescription= "Menlo Park",
                    RegionId=2
                },
                new Territory()
                {
                    Id= "29202",
                    TerritoryDescription= "Columbia",
                    RegionId=4
                },
                new Territory()
                {
                    Id= "03049",
                    TerritoryDescription= "Hollis",
                    RegionId=3
                },
                new Territory()
                {
                    Id= "44122",
                    TerritoryDescription= "Beachwood",
                    RegionId=3
                },
                new Territory()
                {
                    Id= "48304",
                    TerritoryDescription= "Bloomfield Hills",
                    RegionId=3
                },
                new Territory()
                {
                    Id= "72716",
                    TerritoryDescription= "Bentonville",
                    RegionId=4
                }
                );
        }
    }
}

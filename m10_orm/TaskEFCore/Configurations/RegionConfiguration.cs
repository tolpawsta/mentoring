using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TaskEFCore.Models;

namespace TaskEFCore.Configurations
{
    class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.HasKey(r => r.Id)
                .HasName("RedionID");

            builder.Property(r => r.Id)
                .HasColumnName("RegionID");

            builder.Property(r => r.RegionDescription)
                .HasColumnName("RegionDescription")
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}

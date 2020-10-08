using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TaskEFCore.Models;

namespace TaskEFCore.Configurations
{
    class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable("Employees");

            builder.HasKey(e => e.Id)
                .HasName("EmployeeID");

            builder.HasIndex(e => e.LastName);

            builder.Property(e => e.Id)
                .HasColumnName("EmployeeID");

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(e => e.ReportToNavigation)
                .WithMany(r => r.InversReportToNavigations)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasForeignKey(e => e.ReportTo)
                .HasConstraintName("FK_Employees_Employees");
        }
    }
}

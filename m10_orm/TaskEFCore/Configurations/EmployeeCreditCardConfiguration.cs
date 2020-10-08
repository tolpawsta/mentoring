using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskEFCore.Models;

namespace TaskEFCore.Configurations
{
    class EmployeeCreditCardConfiguration : IEntityTypeConfiguration<EmployeeCreditCard>
    {
        public void Configure(EntityTypeBuilder<EmployeeCreditCard> builder)
        {
            builder.HasKey(e => e.CardNumber);

            builder.HasIndex(e => e.EmployeeId)
                .HasName("EmployeeCards");

            builder.HasIndex(e => e.CardHolderName);

            builder.Property(e => e.CardNumber)
                .HasColumnType("bigint")
                .HasMaxLength(16)
                .IsRequired();

            builder.Property(e => e.CardHolderName)
                .HasMaxLength(40);

            builder.Property(e => e.ExpirationDate)
                .HasColumnType("date")
                .HasMaxLength(4)
                .IsRequired();

            builder.HasOne(ec => ec.Employee)
                .WithMany(e => e.CreditCards)
                .HasForeignKey(ec => ec.EmployeeId)
                .HasConstraintName("FK_EmployeeCreditCards_Empoyees");
        }
    }
}

﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskEFCore.Models;

namespace TaskEFCore.Migrations
{
    [DbContext(typeof(Northwind))]
    [Migration("20201008142208_version_1.3")]
    partial class version_13
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TaskEFCore.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CategoryID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("ntext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("CategoryName")
                        .HasColumnType("nvarchar(15)")
                        .HasMaxLength(15);

                    b.HasKey("CategoryID")
                        .HasName("CategoryID");

                    b.HasIndex("Name")
                        .HasName("CategoryName");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TaskEFCore.Models.Customer", b =>
                {
                    b.Property<string>("CustomerID")
                        .HasColumnName("CustomerID")
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<DateTime?>("FoundationDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasColumnName("ContactName")
                        .HasColumnType("nvarchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(24)")
                        .HasMaxLength(24);

                    b.HasKey("CustomerID")
                        .HasName("CustomerID");

                    b.HasIndex("CompanyName")
                        .HasName("CompanyName");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("TaskEFCore.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("EmployeeID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<int?>("ReportTo")
                        .HasColumnType("int");

                    b.HasKey("Id")
                        .HasName("EmployeeID");

                    b.HasIndex("LastName");

                    b.HasIndex("ReportTo");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("TaskEFCore.Models.EmployeeCreditCard", b =>
                {
                    b.Property<long>("CardNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasMaxLength(16);

                    b.Property<string>("CardHolderName")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("date")
                        .HasMaxLength(4);

                    b.HasKey("CardNumber")
                        .HasName("CardNumber");

                    b.HasIndex("CardHolderName");

                    b.HasIndex("EmployeeId")
                        .HasName("EmployeeCards");

                    b.ToTable("EmployeeCreditCards");
                });

            modelBuilder.Entity("TaskEFCore.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("OrderID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(5)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("ShippedDate")
                        .HasColumnType("datetime");

                    b.HasKey("Id")
                        .HasName("OrderID");

                    b.HasIndex("CustomerId")
                        .HasName("CustomerOrders");

                    b.HasIndex("EmployeeId")
                        .HasName("EmployeesOrders");

                    b.HasIndex("OrderDate")
                        .HasName("OrderDate");

                    b.HasIndex("ShippedDate")
                        .HasName("ShippedDate");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("TaskEFCore.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnName("OrderID")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnName("ProductID")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("money");

                    b.HasKey("OrderId", "ProductId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("OrderId")
                        .HasName("OrderID");

                    b.HasIndex("ProductId")
                        .HasName("ProductID");

                    b.ToTable("Order Details");
                });

            modelBuilder.Entity("TaskEFCore.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Coast")
                        .HasColumnName("UnitPrice")
                        .HasColumnType("money");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("ProductName")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ProductID")
                        .HasName("ProductID");

                    b.HasIndex("CategoryId")
                        .HasName("CatagoryID");

                    b.HasIndex("Name")
                        .HasName("ProductName");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("TaskEFCore.Models.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("RegionID")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RegionDescription")
                        .IsRequired()
                        .HasColumnName("RegionDescription")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id")
                        .HasName("RedionID");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("TaskEFCore.Models.Employee", b =>
                {
                    b.HasOne("TaskEFCore.Models.Employee", "ReportToNavigation")
                        .WithMany("InversReportToNavigations")
                        .HasForeignKey("ReportTo")
                        .HasConstraintName("FK_Employees_Employees");
                });

            modelBuilder.Entity("TaskEFCore.Models.EmployeeCreditCard", b =>
                {
                    b.HasOne("TaskEFCore.Models.Employee", "Employee")
                        .WithMany("CreditCards")
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("FK_EmployeeCreditCards_Empoyees")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TaskEFCore.Models.Order", b =>
                {
                    b.HasOne("TaskEFCore.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("FK_Orders_Customers");

                    b.HasOne("TaskEFCore.Models.Employee", "Employee")
                        .WithMany("Orders")
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("FK_Orders_Employees")
                        .IsRequired();
                });

            modelBuilder.Entity("TaskEFCore.Models.OrderDetail", b =>
                {
                    b.HasOne("TaskEFCore.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_Order_Details_Orders")
                        .IsRequired();

                    b.HasOne("TaskEFCore.Models.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_Order_Details_Products")
                        .IsRequired();
                });

            modelBuilder.Entity("TaskEFCore.Models.Product", b =>
                {
                    b.HasOne("TaskEFCore.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_Products_Categories")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using LapTrinhWeb.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LapTrinhWeb.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250531020341_UpdateProductSeedData")]
    partial class UpdateProductSeedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LapTrinhWeb.Models.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = 1,
                            Name = "Thời trang"
                        },
                        new
                        {
                            CategoryId = 2,
                            Name = "Giày dép"
                        },
                        new
                        {
                            CategoryId = 3,
                            Name = "Phụ kiện"
                        });
                });

            modelBuilder.Entity("LapTrinhWeb.Models.Entities.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("LapTrinhWeb.Models.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("LapTrinhWeb.Models.Entities.OrderDetail", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailId"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("LapTrinhWeb.Models.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsFeatured")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Description = "Áo thun chất liệu cotton mềm mại, thấm hút mồ hôi, phù hợp đi chơi hoặc tập thể thao.",
                            Image = "/images/aothun.jpg",
                            IsFeatured = true,
                            Name = "Áo Thun Năng Động",
                            Price = 150000m,
                            ProductId = 1
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            Description = "Thiết kế trẻ trung, phong cách với chất liệu Jean co giãn nhẹ, dễ phối đồ.",
                            Image = "/images/jean.jpg",
                            IsFeatured = false,
                            Name = "Quần Jean Cá Tính",
                            Price = 300000m,
                            ProductId = 2
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 2,
                            Description = "Sneaker siêu nhẹ, đế cao su chống trượt, phù hợp với cả nam và nữ.",
                            Image = "/images/sneaker.jpg",
                            IsFeatured = true,
                            Name = "Giày Sneaker Thời Trang",
                            Price = 500000m,
                            ProductId = 3
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 1,
                            Description = "Áo khoác gió chống nước, nhẹ, phù hợp đi chơi, du lịch.",
                            Image = "/images/aokhoac.jpg",
                            IsFeatured = true,
                            Name = "Áo Khoác Gió",
                            Price = 400000m,
                            ProductId = 4
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 3,
                            Description = "Balo vải bố bền đẹp, nhiều ngăn tiện lợi cho học sinh, sinh viên.",
                            Image = "/images/balo.jpg",
                            IsFeatured = false,
                            Name = "Balo Thời Trang",
                            Price = 250000m,
                            ProductId = 5
                        });
                });

            modelBuilder.Entity("LapTrinhWeb.Models.Entities.Order", b =>
                {
                    b.HasOne("LapTrinhWeb.Models.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("LapTrinhWeb.Models.Entities.OrderDetail", b =>
                {
                    b.HasOne("LapTrinhWeb.Models.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LapTrinhWeb.Models.Entities.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("LapTrinhWeb.Models.Entities.Product", b =>
                {
                    b.HasOne("LapTrinhWeb.Models.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("LapTrinhWeb.Models.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("LapTrinhWeb.Models.Entities.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("LapTrinhWeb.Models.Entities.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("LapTrinhWeb.Models.Entities.Product", b =>
                {
                    b.Navigation("OrderDetails");
                });
#pragma warning restore 612, 618
        }
    }
}

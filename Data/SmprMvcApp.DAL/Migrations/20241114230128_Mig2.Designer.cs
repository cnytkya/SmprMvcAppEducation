﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmprMvcApp.DAL.DbContextModel;

#nullable disable

namespace SmprMvcApp.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241114230128_Mig2")]
    partial class Mig2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SmprMvcApp.EntityLayer.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayOrder = 1,
                            Name = "Teknoloji"
                        },
                        new
                        {
                            Id = 2,
                            DisplayOrder = 2,
                            Name = "Kitap"
                        },
                        new
                        {
                            Id = 3,
                            DisplayOrder = 3,
                            Name = "Tekstil"
                        });
                });

            modelBuilder.Entity("SmprMvcApp.EntityLayer.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ListPrice")
                        .HasColumnType("float");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("Price100")
                        .HasColumnType("float");

                    b.Property<double>("Price50")
                        .HasColumnType("float");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "Cüneyt",
                            CategoryId = 1,
                            Description = "HP Victus 16-R1001NT Intel Core i7 14700HX 16 GB 1 TB SSD",
                            ISBN = "ASS231D65F45DF",
                            ImageUrl = "",
                            ListPrice = 44.0,
                            Price = 55.0,
                            Price100 = 255.0,
                            Price50 = 55.0,
                            Title = "HP Victus"
                        },
                        new
                        {
                            Id = 2,
                            Author = "Kasım",
                            CategoryId = 2,
                            Description = "LENOVO Thinkpad Z16 Gen 1 Ryzen 9 Pro 6950h 32gb 1tb Ssd...",
                            ISBN = "ASS231D65F45DF",
                            ImageUrl = "",
                            ListPrice = 2445.0,
                            Price = 250.0,
                            Price100 = 2522.0,
                            Price50 = 250.0,
                            Title = "Lenova Thingpad"
                        },
                        new
                        {
                            Id = 3,
                            Author = "Kasım",
                            CategoryId = 3,
                            Description = "LENOVO Thinkpad Z16 Gen 1 Ryzen 9 Pro 6950h 32gb 1tb Ssd...",
                            ISBN = "ASS231D65F45DF",
                            ImageUrl = "",
                            ListPrice = 250.0,
                            Price = 25.0,
                            Price100 = 25.0,
                            Price50 = 23.0,
                            Title = "Lenova Thingpad"
                        });
                });

            modelBuilder.Entity("SmprMvcApp.EntityLayer.Entities.Product", b =>
                {
                    b.HasOne("SmprMvcApp.EntityLayer.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pizza.Repository;

#nullable disable

namespace Pizza.Migrations
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20230607130054_Customers")]
    partial class Customers
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Pizza.Models.Menu", b =>
                {
                    b.Property<string>("Menu_Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Pizza_Id")
                        .HasColumnType("int");

                    b.Property<string>("Pizza_Id1")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Tax")
                        .HasColumnType("int");

                    b.Property<int>("Topping_Id")
                        .HasColumnType("int");

                    b.Property<string>("ToppingsTopping_Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Menu_Id");

                    b.HasIndex("Pizza_Id1");

                    b.HasIndex("ToppingsTopping_Id");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("Pizza.Models.Pizza", b =>
                {
                    b.Property<string>("Pizza_Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Crust")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Pizza_Id");

                    b.ToTable("Pizza");
                });

            modelBuilder.Entity("Pizza.Models.Topping", b =>
                {
                    b.Property<string>("Topping_Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Toppings")
                        .HasColumnType("int");

                    b.HasKey("Topping_Id");

                    b.ToTable("Topping");
                });

            modelBuilder.Entity("Pizza.Models.User", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ContactNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Email");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Pizza.Models.Menu", b =>
                {
                    b.HasOne("Pizza.Models.Pizza", "Pizza")
                        .WithMany()
                        .HasForeignKey("Pizza_Id1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pizza.Models.Topping", "Toppings")
                        .WithMany()
                        .HasForeignKey("ToppingsTopping_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pizza");

                    b.Navigation("Toppings");
                });
#pragma warning restore 612, 618
        }
    }
}

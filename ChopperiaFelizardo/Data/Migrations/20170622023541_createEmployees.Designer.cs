﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ChopperiaFelizardo.Models;

namespace ChopperiaFelizardo.Migrations
{
    [DbContext(typeof(ChopperiaContext))]
    [Migration("20170622023541_createEmployees")]
    partial class createEmployees
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ChopperiaFelizardo.Models.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ChopperiaFelizardo.Models.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("Name");

                    b.Property<string>("Neighborhood");

                    b.Property<string>("PhoneHouse");

                    b.Property<string>("PhoneMobile");

                    b.Property<string>("RG");

                    b.Property<string>("State");

                    b.Property<string>("ZipeCode");

                    b.Property<string>("number");

                    b.HasKey("ID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ChopperiaFelizardo.Models.FormPayment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("FormsPayments");
                });

            modelBuilder.Entity("ChopperiaFelizardo.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryID");

                    b.Property<string>("Description");

                    b.Property<string>("Image");

                    b.Property<string>("Name");

                    b.Property<decimal>("PriceBuy");

                    b.Property<decimal>("PriceSell");

                    b.Property<decimal>("Profit");

                    b.Property<int>("SectorID");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("SectorID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ChopperiaFelizardo.Models.Sector", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Sectors");
                });

            modelBuilder.Entity("ChopperiaFelizardo.Models.Situation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Situations");
                });

            modelBuilder.Entity("ChopperiaFelizardo.Models.Table", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Identifier");

                    b.Property<string>("NickName");

                    b.Property<string>("Status");

                    b.HasKey("ID");

                    b.ToTable("Tables");
                });

            modelBuilder.Entity("ChopperiaFelizardo.Models.Type", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("ChopperiaFelizardo.Models.Product", b =>
                {
                    b.HasOne("ChopperiaFelizardo.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ChopperiaFelizardo.Models.Sector", "Sector")
                        .WithMany()
                        .HasForeignKey("SectorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

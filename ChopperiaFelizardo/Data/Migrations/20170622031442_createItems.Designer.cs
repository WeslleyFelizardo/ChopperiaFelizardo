﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ChopperiaFelizardo.Models;

namespace ChopperiaFelizardo.Migrations
{
    [DbContext(typeof(ChopperiaContext))]
    [Migration("20170622031442_createItems")]
    partial class createItems
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ChopperiaFelizardo.Models.Box", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("EmployeeID");

                    b.Property<decimal>("ValueCurrent");

                    b.Property<decimal>("ValueEnd");

                    b.Property<decimal>("ValueInitial");

                    b.Property<bool>("isOpen");

                    b.HasKey("ID");

                    b.HasIndex("EmployeeID");

                    b.ToTable("Boxs");
                });

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

            modelBuilder.Entity("ChopperiaFelizardo.Models.Item", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Observation");

                    b.Property<int>("OrderID");

                    b.Property<int>("ProductID");

                    b.HasKey("ID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("ChopperiaFelizardo.Models.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Observation");

                    b.Property<int>("SituationID");

                    b.Property<int>("TableID");

                    b.Property<decimal>("Value");

                    b.HasKey("ID");

                    b.HasIndex("SituationID");

                    b.HasIndex("TableID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ChopperiaFelizardo.Models.OrderPayment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BoxID");

                    b.Property<DateTime>("Date");

                    b.Property<int>("FormPaymentID");

                    b.Property<int>("OrderID");

                    b.Property<decimal>("Value");

                    b.HasKey("ID");

                    b.HasIndex("BoxID");

                    b.HasIndex("FormPaymentID");

                    b.HasIndex("OrderID");

                    b.ToTable("OrdersPayments");
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

            modelBuilder.Entity("ChopperiaFelizardo.Models.Transation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BoxID");

                    b.Property<int>("TypeID");

                    b.HasKey("ID");

                    b.HasIndex("BoxID");

                    b.HasIndex("TypeID");

                    b.ToTable("Transations");
                });

            modelBuilder.Entity("ChopperiaFelizardo.Models.Type", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("ID");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("ChopperiaFelizardo.Models.Box", b =>
                {
                    b.HasOne("ChopperiaFelizardo.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ChopperiaFelizardo.Models.Item", b =>
                {
                    b.HasOne("ChopperiaFelizardo.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ChopperiaFelizardo.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ChopperiaFelizardo.Models.Order", b =>
                {
                    b.HasOne("ChopperiaFelizardo.Models.Situation", "Situation")
                        .WithMany()
                        .HasForeignKey("SituationID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ChopperiaFelizardo.Models.Table", "Table")
                        .WithMany()
                        .HasForeignKey("TableID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ChopperiaFelizardo.Models.OrderPayment", b =>
                {
                    b.HasOne("ChopperiaFelizardo.Models.Box", "Box")
                        .WithMany()
                        .HasForeignKey("BoxID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ChopperiaFelizardo.Models.FormPayment", "FormPayment")
                        .WithMany()
                        .HasForeignKey("FormPaymentID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ChopperiaFelizardo.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade);
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

            modelBuilder.Entity("ChopperiaFelizardo.Models.Transation", b =>
                {
                    b.HasOne("ChopperiaFelizardo.Models.Box", "Box")
                        .WithMany("Transations")
                        .HasForeignKey("BoxID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ChopperiaFelizardo.Models.Type", "Type")
                        .WithMany()
                        .HasForeignKey("TypeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}

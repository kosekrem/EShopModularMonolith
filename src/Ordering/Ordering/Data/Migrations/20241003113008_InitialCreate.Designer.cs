﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Ordering.Data;

#nullable disable

namespace Ordering.Data.Migrations
{
    [DbContext(typeof(OrderingDbContext))]
    [Migration("20241003113008_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("ordering")
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Ordering.Orders.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("OrderName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.ComplexProperty<Dictionary<string, object>>("BillingAddress", "Ordering.Orders.Models.Order.BillingAddress#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("AddressLine")
                                .IsRequired()
                                .HasMaxLength(180)
                                .HasColumnType("character varying(180)");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("EmailAddress")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasMaxLength(5)
                                .HasColumnType("character varying(5)");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Payment", "Ordering.Orders.Models.Order.Payment#Payment", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("CVV")
                                .IsRequired()
                                .HasMaxLength(3)
                                .HasColumnType("character varying(3)");

                            b1.Property<string>("CardName")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("CardNumber")
                                .IsRequired()
                                .HasMaxLength(24)
                                .HasColumnType("character varying(24)");

                            b1.Property<string>("Expiration")
                                .IsRequired()
                                .HasMaxLength(10)
                                .HasColumnType("character varying(10)");

                            b1.Property<int>("PaymentMethod")
                                .HasColumnType("integer");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("ShippingAddress", "Ordering.Orders.Models.Order.ShippingAddress#Address", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("AddressLine")
                                .IsRequired()
                                .HasMaxLength(180)
                                .HasColumnType("character varying(180)");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("EmailAddress")
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasMaxLength(5)
                                .HasColumnType("character varying(5)");
                        });

                    b.HasKey("Id");

                    b.HasIndex("OrderName")
                        .IsUnique();

                    b.ToTable("Orders", "ordering");
                });

            modelBuilder.Entity("Ordering.Orders.Models.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("text");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uuid");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems", "ordering");
                });

            modelBuilder.Entity("Ordering.Orders.Models.OrderItem", b =>
                {
                    b.HasOne("Ordering.Orders.Models.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Ordering.Orders.Models.Order", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}

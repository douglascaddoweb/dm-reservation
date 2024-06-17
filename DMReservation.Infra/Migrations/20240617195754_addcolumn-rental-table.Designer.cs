﻿// <auto-generated />
using System;
using System.Collections.Generic;
using DMReservation.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DMReservation.Infra.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240617195754_addcolumn-rental-table")]
    partial class addcolumnrentaltable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DMReservation.Domain.Entities.DeliveryMan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birthdate");

                    b.Property<string>("Image")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("image");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("name");

                    b.ComplexProperty<Dictionary<string, object>>("Cnh", "DMReservation.Domain.Entities.DeliveryMan.Cnh#Cnh", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("varchar(20)")
                                .HasColumnName("cnh");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("Cnpj", "DMReservation.Domain.Entities.DeliveryMan.Cnpj#Cnpj", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("varchar(20)")
                                .HasColumnName("cnpj");
                        });

                    b.ComplexProperty<Dictionary<string, object>>("TypeCnh", "DMReservation.Domain.Entities.DeliveryMan.TypeCnh#TypeCnh", b1 =>
                        {
                            b1.IsRequired();

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("varchar(2)")
                                .HasColumnName("typecnh");
                        });

                    b.HasKey("Id");

                    b.ToTable("deliveryman", (string)null);
                });

            modelBuilder.Entity("DMReservation.Domain.Entities.Motorcycle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("LicensePlate")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasColumnName("licenseplate");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("model");

                    b.Property<short>("Year")
                        .HasColumnType("smallint")
                        .HasColumnName("year");

                    b.HasKey("Id");

                    b.ToTable("motorcycle", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
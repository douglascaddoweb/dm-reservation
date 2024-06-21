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
    [Migration("20240620235625_create-notifyorder-table")]
    partial class createnotifyordertable
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
                        .HasColumnType("integer")
                        .HasColumnName("id");

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

            modelBuilder.Entity("DMReservation.Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("createdat");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric(18,2)")
                        .HasColumnName("price");

                    b.Property<short>("Status")
                        .HasColumnType("short")
                        .HasColumnName("status");

                    b.HasKey("Id");

                    b.ToTable("order", (string)null);
                });

            modelBuilder.Entity("DMReservation.Domain.Entities.OrderDeliveryMan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DeliveryManId")
                        .HasColumnType("integer")
                        .HasColumnName("iddeliveryman");

                    b.Property<int?>("DeliveryManId1")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer")
                        .HasColumnName("idorder");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryManId");

                    b.HasIndex("DeliveryManId1");

                    b.HasIndex("OrderId");

                    b.ToTable("orderdeliveryman", (string)null);
                });

            modelBuilder.Entity("DMReservation.Domain.Entities.Rental", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int4")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp")
                        .HasColumnName("createdat");

                    b.Property<DateTime?>("DateFinish")
                        .HasColumnType("timestamp")
                        .HasColumnName("datefinish");

                    b.Property<DateTime>("DateForecastFinish")
                        .HasColumnType("timestamp")
                        .HasColumnName("dateforecastfinish");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("timestamp")
                        .HasColumnName("datestart");

                    b.Property<int>("DeliveryManId")
                        .HasColumnType("int4")
                        .HasColumnName("iddeliveryman");

                    b.Property<int?>("DeliveryManId1")
                        .HasColumnType("int");

                    b.Property<decimal>("ExtraPrice")
                        .HasColumnType("numeric(18,2)")
                        .HasColumnName("extraprice");

                    b.Property<decimal>("Fine")
                        .HasColumnType("numeric(5,2)")
                        .HasColumnName("fine");

                    b.Property<int>("MotorcycleId")
                        .HasColumnType("int4")
                        .HasColumnName("idmotorcycle");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric(18,2)")
                        .HasColumnName("price");

                    b.Property<short>("RentalPlanId")
                        .HasColumnType("int2")
                        .HasColumnName("idrentalplan");

                    b.Property<short>("Status")
                        .HasColumnType("int2")
                        .HasColumnName("status");

                    b.Property<decimal>("Total")
                        .HasColumnType("numeric(18,2)")
                        .HasColumnName("total");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryManId");

                    b.HasIndex("DeliveryManId1");

                    b.HasIndex("MotorcycleId");

                    b.HasIndex("RentalPlanId");

                    b.ToTable("rental", (string)null);
                });

            modelBuilder.Entity("DMReservation.Domain.Entities.RentalPlan", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int2")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<short>("Id"));

                    b.Property<short>("Days")
                        .HasColumnType("int2")
                        .HasColumnName("days");

                    b.Property<decimal>("ExtraPrice")
                        .HasColumnType("numeric(18,2)")
                        .HasColumnName("extraprice");

                    b.Property<decimal>("Fine")
                        .HasColumnType("numeric(5,2)")
                        .HasColumnName("fine");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric(18,2)")
                        .HasColumnName("price");

                    b.HasKey("Id");

                    b.ToTable("rentalplan", (string)null);
                });

            modelBuilder.Entity("DMReservation.Domain.Entities.OrderDeliveryMan", b =>
                {
                    b.HasOne("DMReservation.Domain.Entities.DeliveryMan", "DeliveryMan")
                        .WithMany()
                        .HasForeignKey("DeliveryManId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DMReservation.Domain.Entities.DeliveryMan", null)
                        .WithMany("OrderDeliveryMan")
                        .HasForeignKey("DeliveryManId1");

                    b.HasOne("DMReservation.Domain.Entities.Order", "Order")
                        .WithMany("OrderDeliveryMan")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeliveryMan");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("DMReservation.Domain.Entities.Rental", b =>
                {
                    b.HasOne("DMReservation.Domain.Entities.DeliveryMan", "DeliveryMan")
                        .WithMany()
                        .HasForeignKey("DeliveryManId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DMReservation.Domain.Entities.DeliveryMan", null)
                        .WithMany("Rental")
                        .HasForeignKey("DeliveryManId1");

                    b.HasOne("DMReservation.Domain.Entities.Motorcycle", "Motorcycle")
                        .WithMany("Rentals")
                        .HasForeignKey("MotorcycleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DMReservation.Domain.Entities.RentalPlan", "RentalPlan")
                        .WithMany()
                        .HasForeignKey("RentalPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DeliveryMan");

                    b.Navigation("Motorcycle");

                    b.Navigation("RentalPlan");
                });

            modelBuilder.Entity("DMReservation.Domain.Entities.DeliveryMan", b =>
                {
                    b.Navigation("OrderDeliveryMan");

                    b.Navigation("Rental");
                });

            modelBuilder.Entity("DMReservation.Domain.Entities.Motorcycle", b =>
                {
                    b.Navigation("Rentals");
                });

            modelBuilder.Entity("DMReservation.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderDeliveryMan");
                });
#pragma warning restore 612, 618
        }
    }
}
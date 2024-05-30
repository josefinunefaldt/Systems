﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Subscribers.Data;

#nullable disable

namespace Subscribers.Migrations
{
    [DbContext(typeof(SubDbContext))]
    [Migration("20231126112112_InitialSub")]
    partial class InitialSub
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Subscribers.Data.Entities.Sub", b =>
                {
                    b.Property<int>("sub_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("sub_id"));

                    b.Property<string>("sub_FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("sub_LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("sub_PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("sub_deliveryAddress")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("sub_postalCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("sub_socialSecurityNumber")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("sub_subscriptionNumber")
                        .HasColumnType("int");

                    b.HasKey("sub_id");

                    b.HasIndex("sub_FirstName")
                        .IsUnique();

                    b.ToTable("tbl_subscribers");

                    b.HasData(
                        new
                        {
                            sub_id = 1,
                            sub_FirstName = "Maja",
                            sub_LastName = "Svensson",
                            sub_PhoneNumber = "0768234535",
                            sub_deliveryAddress = "fallrundan 11",
                            sub_postalCode = "82830",
                            sub_socialSecurityNumber = "930412",
                            sub_subscriptionNumber = 100
                        },
                        new
                        {
                            sub_id = 2,
                            sub_FirstName = "Kalle",
                            sub_LastName = "Andersson",
                            sub_PhoneNumber = "0738234536",
                            sub_deliveryAddress = "dalrundan 27",
                            sub_postalCode = "82832",
                            sub_socialSecurityNumber = "930418",
                            sub_subscriptionNumber = 337
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
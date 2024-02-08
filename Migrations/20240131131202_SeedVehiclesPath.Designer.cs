﻿// <auto-generated />
using System;
using Autopark.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NetTopologySuite.Geometries;

#nullable disable

namespace Autopark.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240131131202_SeedVehiclesPath")]
    partial class SeedVehiclesPath
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Autopark.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "f58b5106-1d5c-49f4-8416-e66d62769794",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "SAM",
                            PasswordHash = "AQAAAAIAAYagAAAAEF5twEtYO7+mwyDJAEG+VjR/JIJsSw5/LVSTjDTHSVE27mJT/cyJ3ygSGVvvf6nYPQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "a845c569-bd58-422f-ac7b-2f274b8a9347",
                            TwoFactorEnabled = false,
                            UserName = "Manager Sam"
                        },
                        new
                        {
                            Id = "2",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "54e50f68-8d29-42cc-8bd8-36dcb4f5762c",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "TOM",
                            PasswordHash = "AQAAAAIAAYagAAAAEOz3LKwZoRYxx/fMjskCu844n1B35YZSJcYA+rXnQtb0MS2u/zCXvHTbIIO+IqvOsQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "834a8a4d-baf9-4dff-a99c-add5ccc93261",
                            TwoFactorEnabled = false,
                            UserName = "Manager Tom"
                        });
                });

            modelBuilder.Entity("Autopark.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequiredDriverCategory")
                        .HasColumnType("int");

                    b.Property<int>("SeatsAmount")
                        .HasColumnType("int");

                    b.Property<int>("Segment")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Brands");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Chery",
                            RequiredDriverCategory = 0,
                            SeatsAmount = 5,
                            Segment = 0,
                            Type = 3
                        },
                        new
                        {
                            Id = 2,
                            Name = "Kamaz",
                            RequiredDriverCategory = 4,
                            SeatsAmount = 2,
                            Segment = 2,
                            Type = 8
                        },
                        new
                        {
                            Id = 3,
                            Name = "Ferrari",
                            RequiredDriverCategory = 0,
                            SeatsAmount = 2,
                            Segment = 1,
                            Type = 2
                        });
                });

            modelBuilder.Entity("Autopark.Models.Driver", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("EnterpriseId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EnterpriseId");

                    b.ToTable("Drivers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EnterpriseId = 1,
                            Name = "Василий",
                            Salary = 45000
                        },
                        new
                        {
                            Id = 2,
                            EnterpriseId = 2,
                            Name = "Геннадий",
                            Salary = 30000
                        },
                        new
                        {
                            Id = 3,
                            EnterpriseId = 3,
                            Name = "Евгений",
                            Salary = 50000
                        },
                        new
                        {
                            Id = 4,
                            EnterpriseId = 1,
                            Name = "Александр",
                            Salary = 55000
                        },
                        new
                        {
                            Id = 5,
                            EnterpriseId = 2,
                            Name = "Григорий",
                            Salary = 35000
                        });
                });

            modelBuilder.Entity("Autopark.Models.Enterprise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Enterprises");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Москва",
                            Name = "Мосгортранс"
                        },
                        new
                        {
                            Id = 2,
                            City = "Мюнхен",
                            Name = "DHL"
                        },
                        new
                        {
                            Id = 3,
                            City = "Новосибирск",
                            Name = "Сибтранс"
                        });
                });

            modelBuilder.Entity("Autopark.Models.EnterpriseManager", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ManagedEnterpriseId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ManagedEnterpriseId");

                    b.HasIndex("ManagedEnterpriseId");

                    b.ToTable("EnterpriseManager");

                    b.HasData(
                        new
                        {
                            UserId = "1",
                            ManagedEnterpriseId = 1
                        },
                        new
                        {
                            UserId = "1",
                            ManagedEnterpriseId = 2
                        },
                        new
                        {
                            UserId = "2",
                            ManagedEnterpriseId = 2
                        },
                        new
                        {
                            UserId = "2",
                            ManagedEnterpriseId = 3
                        });
                });

            modelBuilder.Entity("Autopark.Models.Geopoint", b =>
                {
                    b.Property<Guid>("UUID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DriverId")
                        .HasColumnType("int");

                    b.Property<Point>("Point")
                        .IsRequired()
                        .HasColumnType("geography");

                    b.Property<DateTime>("RegisterTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("UUID");

                    b.ToTable("Points");

                    b.HasData(
                        new
                        {
                            UUID = new Guid("b2768ea2-678a-4520-a469-48dd92db5c41"),
                            DriverId = 1,
                            Point = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (1 2)"),
                            RegisterTime = new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            VehicleId = 1
                        },
                        new
                        {
                            UUID = new Guid("5f0814c7-a66f-4bac-894c-227091ad8c53"),
                            DriverId = 1,
                            Point = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (3 4)"),
                            RegisterTime = new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            VehicleId = 1
                        },
                        new
                        {
                            UUID = new Guid("95958216-3829-489a-a518-776d3349a957"),
                            DriverId = 1,
                            Point = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (5 6)"),
                            RegisterTime = new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            VehicleId = 1
                        },
                        new
                        {
                            UUID = new Guid("34451e00-16e8-401d-9a64-812bc5c6d72f"),
                            DriverId = 1,
                            Point = (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (7 8)"),
                            RegisterTime = new DateTime(2024, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            VehicleId = 1
                        });
                });

            modelBuilder.Entity("Autopark.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AcquireTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int?>("DriverId")
                        .HasColumnType("int");

                    b.Property<int?>("EnterpriseId")
                        .HasColumnType("int");

                    b.Property<int>("HorsePower")
                        .HasColumnType("int");

                    b.Property<int>("Mileage")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.Property<float>("ZeroToHundred")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("DriverId")
                        .IsUnique()
                        .HasFilter("[DriverId] IS NOT NULL");

                    b.HasIndex("EnterpriseId");

                    b.ToTable("Vehicles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AcquireTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BrandId = 1,
                            DriverId = 1,
                            EnterpriseId = 1,
                            HorsePower = 5000,
                            Mileage = 2000,
                            Name = "Chery Tiggo Pro Max 8",
                            Price = 3120000,
                            Year = 2023,
                            ZeroToHundred = 8.5f
                        },
                        new
                        {
                            Id = 2,
                            AcquireTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BrandId = 1,
                            EnterpriseId = 1,
                            HorsePower = 5500,
                            Mileage = 4000,
                            Name = "Chery Tiggo Pro Max 7",
                            Price = 2055000,
                            Year = 2022,
                            ZeroToHundred = 9.8f
                        },
                        new
                        {
                            Id = 3,
                            AcquireTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BrandId = 1,
                            DriverId = 2,
                            EnterpriseId = 2,
                            HorsePower = 5000,
                            Mileage = 10000,
                            Name = "Chery Tiggo Pro Max 6",
                            Price = 3120000,
                            Year = 2021,
                            ZeroToHundred = 8.5f
                        },
                        new
                        {
                            Id = 4,
                            AcquireTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BrandId = 2,
                            EnterpriseId = 2,
                            HorsePower = 2500,
                            Mileage = 14000,
                            Name = "КАМАЗ 69592",
                            Price = 3055000,
                            Year = 2012,
                            ZeroToHundred = 26f
                        },
                        new
                        {
                            Id = 5,
                            AcquireTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            BrandId = 3,
                            DriverId = 3,
                            EnterpriseId = 3,
                            HorsePower = 10000,
                            Mileage = 0,
                            Name = "FERRARI F430",
                            Price = 130120000,
                            Year = 2020,
                            ZeroToHundred = 1.8f
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityRole");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "1",
                            RoleId = "1"
                        },
                        new
                        {
                            UserId = "2",
                            RoleId = "1"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("VehicleDriver", b =>
                {
                    b.Property<int>("AssignedDriversId")
                        .HasColumnType("int");

                    b.Property<int>("AssignedCarsId")
                        .HasColumnType("int");

                    b.HasKey("AssignedDriversId", "AssignedCarsId");

                    b.HasIndex("AssignedCarsId");

                    b.ToTable("VehicleDriver");

                    b.HasData(
                        new
                        {
                            AssignedDriversId = 1,
                            AssignedCarsId = 1
                        },
                        new
                        {
                            AssignedDriversId = 2,
                            AssignedCarsId = 1
                        },
                        new
                        {
                            AssignedDriversId = 3,
                            AssignedCarsId = 2
                        },
                        new
                        {
                            AssignedDriversId = 4,
                            AssignedCarsId = 2
                        },
                        new
                        {
                            AssignedDriversId = 5,
                            AssignedCarsId = 6
                        },
                        new
                        {
                            AssignedDriversId = 1,
                            AssignedCarsId = 4
                        },
                        new
                        {
                            AssignedDriversId = 2,
                            AssignedCarsId = 4
                        },
                        new
                        {
                            AssignedDriversId = 3,
                            AssignedCarsId = 5
                        },
                        new
                        {
                            AssignedDriversId = 4,
                            AssignedCarsId = 5
                        });
                });

            modelBuilder.Entity("Autopark.Models.Roles.Manager", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityRole");

                    b.HasDiscriminator().HasValue("Manager");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Name = "manager"
                        });
                });

            modelBuilder.Entity("Autopark.Models.Driver", b =>
                {
                    b.HasOne("Autopark.Models.Enterprise", "Enterprise")
                        .WithMany("Drivers")
                        .HasForeignKey("EnterpriseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enterprise");
                });

            modelBuilder.Entity("Autopark.Models.EnterpriseManager", b =>
                {
                    b.HasOne("Autopark.Models.Enterprise", "Enterprise")
                        .WithMany("EnterpriseManagers")
                        .HasForeignKey("ManagedEnterpriseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Autopark.Models.AppUser", "Manager")
                        .WithMany("ManagedCompanies")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enterprise");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("Autopark.Models.Vehicle", b =>
                {
                    b.HasOne("Autopark.Models.Brand", "Brand")
                        .WithMany("Vehicles")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Autopark.Models.Driver", "Driver")
                        .WithOne("Vehicle")
                        .HasForeignKey("Autopark.Models.Vehicle", "DriverId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Autopark.Models.Enterprise", "Enterprise")
                        .WithMany("Vehicles")
                        .HasForeignKey("EnterpriseId");

                    b.Navigation("Brand");

                    b.Navigation("Driver");

                    b.Navigation("Enterprise");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Autopark.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Autopark.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Autopark.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Autopark.Models.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VehicleDriver", b =>
                {
                    b.HasOne("Autopark.Models.Vehicle", null)
                        .WithMany()
                        .HasForeignKey("AssignedCarsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Autopark.Models.Driver", null)
                        .WithMany()
                        .HasForeignKey("AssignedDriversId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Autopark.Models.AppUser", b =>
                {
                    b.Navigation("ManagedCompanies");
                });

            modelBuilder.Entity("Autopark.Models.Brand", b =>
                {
                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("Autopark.Models.Driver", b =>
                {
                    b.Navigation("Vehicle");
                });

            modelBuilder.Entity("Autopark.Models.Enterprise", b =>
                {
                    b.Navigation("Drivers");

                    b.Navigation("EnterpriseManagers");

                    b.Navigation("Vehicles");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using BoostHolding.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BoostHolding.DataAccessLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220919194538_role")]
    partial class role
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BoostHolding.Entities.Data.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "HUMAN RESOURCES DEPARTMENT"
                        },
                        new
                        {
                            Id = 2,
                            Name = "MANAGEMENT DEPARTMENT"
                        },
                        new
                        {
                            Id = 3,
                            Name = "IT DEPARTMENT"
                        },
                        new
                        {
                            Id = 4,
                            Name = "FINANCE DEPARTMENT"
                        },
                        new
                        {
                            Id = 5,
                            Name = "ACCOUNTING DEPARTMENT"
                        },
                        new
                        {
                            Id = 6,
                            Name = "PRODUCTION DEPARTMENT"
                        },
                        new
                        {
                            Id = 7,
                            Name = "PUBLIC RELATIONS DEPARTMENT"
                        },
                        new
                        {
                            Id = 8,
                            Name = "LAW DEPARTMENT"
                        },
                        new
                        {
                            Id = 9,
                            Name = "RESEARCH AND DEVELOPMENT DEPARTMENT"
                        },
                        new
                        {
                            Id = 10,
                            Name = "PUBLIC RELATIONS DEPARTMENT"
                        });
                });

            modelBuilder.Entity("BoostHolding.Entities.Data.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfLeave")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TcNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TitleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("TitleId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("BoostHolding.Entities.Data.Title", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Titles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Sales Consultant"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Secretary"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Manager"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Team Leader"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Software engineer"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Project Manager"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Engineer"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Chef"
                        },
                        new
                        {
                            Id = 9,
                            Name = "Office Worker"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Worker"
                        },
                        new
                        {
                            Id = 11,
                            Name = "Human Resources Responsible"
                        },
                        new
                        {
                            Id = 12,
                            Name = "General Manager"
                        },
                        new
                        {
                            Id = 13,
                            Name = "Accounting Manager"
                        },
                        new
                        {
                            Id = 14,
                            Name = "Supervisor"
                        },
                        new
                        {
                            Id = 15,
                            Name = "Finance Director"
                        });
                });

            modelBuilder.Entity("BoostHolding.Entities.Data.Employee", b =>
                {
                    b.HasOne("BoostHolding.Entities.Data.Department", "Department")
                        .WithMany("Employees")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BoostHolding.Entities.Data.Title", "Title")
                        .WithMany("Employees")
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Title");
                });

            modelBuilder.Entity("BoostHolding.Entities.Data.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("BoostHolding.Entities.Data.Title", b =>
                {
                    b.Navigation("Employees");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using BoostHolding.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BoostHolding.DataAccessLayer.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BoostHolding.Entities.Data.AdvancePayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Salary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("AdvancePayments");
                });

            modelBuilder.Entity("BoostHolding.Entities.Data.Demand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateOfDemand")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Demands");
                });

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

                    b.Property<int?>("AdvancePaymentId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfLeave")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfStart")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ExpenditureId")
                        .HasColumnType("int");

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

                    b.Property<int?>("PermissionId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TcNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TitleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("TitleId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("BoostHolding.Entities.Data.Expenditure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Document")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpenditureDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ExpenditureTypeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Unit")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ExpenditureTypeId");

                    b.ToTable("Expenditures");
                });

            modelBuilder.Entity("BoostHolding.Entities.Data.ExpenditureType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ExpenditureTypes");
                });

            modelBuilder.Entity("BoostHolding.Entities.Data.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ApprovalStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfRequest")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfStart")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("TotalDaysOff")
                        .HasColumnType("int");

                    b.Property<int>("TypesOfPermissionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("TypesOfPermissionId");

                    b.ToTable("Permissions");
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

            modelBuilder.Entity("BoostHolding.Entities.Data.TypesOfPermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypeOfPermissions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Annual Permission"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Maternity Permission"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Paternity Permission"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Marriage Permission"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Disease Permission"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Excuse Permissions"
                        });
                });

            modelBuilder.Entity("BoostHolding.Entities.Identity.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("BoostHolding.Entities.Identity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime>("SignedInTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("RolesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("BoostHolding.Entities.Data.AdvancePayment", b =>
                {
                    b.HasOne("BoostHolding.Entities.Data.Employee", "Employee")
                        .WithMany("AdvancePayments")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("BoostHolding.Entities.Data.Employee", b =>
                {
                    b.HasOne("BoostHolding.Entities.Data.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId");

                    b.HasOne("BoostHolding.Entities.Data.Title", "Title")
                        .WithMany("Employees")
                        .HasForeignKey("TitleId");

                    b.Navigation("Department");

                    b.Navigation("Title");
                });

            modelBuilder.Entity("BoostHolding.Entities.Data.Expenditure", b =>
                {
                    b.HasOne("BoostHolding.Entities.Data.Employee", "Employee")
                        .WithMany("Expenditures")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BoostHolding.Entities.Data.ExpenditureType", "ExpenditureType")
                        .WithMany("Expenditures")
                        .HasForeignKey("ExpenditureTypeId");

                    b.Navigation("Employee");

                    b.Navigation("ExpenditureType");
                });

            modelBuilder.Entity("BoostHolding.Entities.Data.Permission", b =>
                {
                    b.HasOne("BoostHolding.Entities.Data.Employee", "Employee")
                        .WithMany("Permission")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BoostHolding.Entities.Data.TypesOfPermission", "TypesOfPermission")
                        .WithMany("Permissions")
                        .HasForeignKey("TypesOfPermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("TypesOfPermission");
                });

            modelBuilder.Entity("RoleUser", b =>
                {
                    b.HasOne("BoostHolding.Entities.Identity.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BoostHolding.Entities.Identity.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BoostHolding.Entities.Data.Employee", b =>
                {
                    b.Navigation("AdvancePayments");

                    b.Navigation("Expenditures");

                    b.Navigation("Permission");
                });

            modelBuilder.Entity("BoostHolding.Entities.Data.ExpenditureType", b =>
                {
                    b.Navigation("Expenditures");
                });

            modelBuilder.Entity("BoostHolding.Entities.Data.Title", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("BoostHolding.Entities.Data.TypesOfPermission", b =>
                {
                    b.Navigation("Permissions");
                });
#pragma warning restore 612, 618
        }
    }
}

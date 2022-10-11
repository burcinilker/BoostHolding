using BoostHolding.Entities;
using BoostHolding.Entities.Data;
using BoostHolding.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoostHolding.DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
       
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AdvancePayment> AdvancePayments { get; set; }
        public DbSet<Expenditure> Expenditures { get; set; }
        public DbSet<ExpenditureType> ExpenditureTypes { get; set; }
        public DbSet<Demand> Demands { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<TypesOfPermission> TypeOfPermissions { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TypesOfPermission>().HasData(
                new TypesOfPermission() { Id=1, Name= "Annual Permission" },
                new TypesOfPermission() { Id=2, Name= "Maternity Permission" },
                new TypesOfPermission() { Id=3, Name= "Paternity Permission" },
                new TypesOfPermission() { Id=4, Name= "Marriage Permission" },
                new TypesOfPermission() { Id=5, Name= "Disease Permission" },
                new TypesOfPermission() { Id=6, Name= "Excuse Permissions" }
                );

            modelBuilder.Entity<Department>().HasData(
                new Department() { Id =1, Name= "HUMAN RESOURCES DEPARTMENT" },
                new Department() { Id =2, Name = "MANAGEMENT DEPARTMENT" },
                new Department() { Id =3, Name = "IT DEPARTMENT" },
                new Department() { Id =4, Name = "FINANCE DEPARTMENT" },
                new Department() { Id =5, Name = "ACCOUNTING DEPARTMENT" },
                new Department() { Id =6, Name = "PRODUCTION DEPARTMENT" },
                new Department() { Id =7, Name = "PUBLIC RELATIONS DEPARTMENT" },
                new Department() { Id =8, Name = "LAW DEPARTMENT" },
                new Department() { Id =9, Name = "RESEARCH AND DEVELOPMENT DEPARTMENT" },
                new Department() { Id =10, Name = "PUBLIC RELATIONS DEPARTMENT" }
                );

            modelBuilder.Entity<Title>().HasData(
                new Title() { Id = 1, Name = "Sales Consultant" },
                new Title() { Id = 2, Name = "Secretary" },
                new Title() { Id = 3, Name ="Manager"},
                new Title() { Id = 4, Name = "Team Leader" },
                new Title() { Id = 5, Name = "Software engineer" },
                new Title() { Id = 6, Name = "Project Manager" },
                new Title() { Id = 7, Name = "Engineer" },
                new Title() { Id = 8, Name ="Chef"},
                new Title() { Id = 9, Name ="Office Worker"},
                new Title() { Id = 10, Name ="Worker"},
                new Title() { Id = 11, Name = "Human Resources Responsible" },
                new Title() { Id = 12, Name = "General Manager" },
                new Title() { Id = 13, Name = "Accounting Manager" },
                new Title() { Id = 14, Name = "Supervisor" },
                new Title() { Id = 15, Name = "Finance Director" }
                );
        }

    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoostHolding.DataAccessLayer.Migrations
{
    public partial class permissionsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenditures_Employees_EmployeeId",
                table: "Expenditures");

            migrationBuilder.DropIndex(
                name: "IX_Expenditures_EmployeeId",
                table: "Expenditures");

            migrationBuilder.AddColumn<int>(
                name: "PermissionId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EmployeeExpenditure",
                columns: table => new
                {
                    EmployeesId = table.Column<int>(type: "int", nullable: false),
                    ExpendituresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeExpenditure", x => new { x.EmployeesId, x.ExpendituresId });
                    table.ForeignKey(
                        name: "FK_EmployeeExpenditure_Employees_EmployeesId",
                        column: x => x.EmployeesId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeExpenditure_Expenditures_ExpendituresId",
                        column: x => x.ExpendituresId,
                        principalTable: "Expenditures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeOfPermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOfPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    TypesOfPermissionId = table.Column<int>(type: "int", nullable: false),
                    DateOfRequest = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovalStatus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_TypeOfPermissions_TypesOfPermissionId",
                        column: x => x.TypesOfPermissionId,
                        principalTable: "TypeOfPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TypeOfPermissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Annual Permission" },
                    { 2, "Maternity Permission" },
                    { 3, "Paternity Permission" },
                    { 4, "Marriage Permission" },
                    { 5, "Disease Permission" },
                    { 6, "Excuse Permissions" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PermissionId",
                table: "Employees",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeExpenditure_ExpendituresId",
                table: "EmployeeExpenditure",
                column: "ExpendituresId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_TypesOfPermissionId",
                table: "Permissions",
                column: "TypesOfPermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Permissions_PermissionId",
                table: "Employees",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Permissions_PermissionId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "EmployeeExpenditure");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "TypeOfPermissions");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PermissionId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Expenditures_EmployeeId",
                table: "Expenditures",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenditures_Employees_EmployeeId",
                table: "Expenditures",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

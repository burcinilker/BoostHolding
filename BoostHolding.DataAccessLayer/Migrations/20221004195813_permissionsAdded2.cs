using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoostHolding.DataAccessLayer.Migrations
{
    public partial class permissionsAdded2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Permissions_PermissionId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PermissionId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "TotalDaysOff",
                table: "Permissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_EmployeeId",
                table: "Permissions",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Permissions_Employees_EmployeeId",
                table: "Permissions",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Permissions_Employees_EmployeeId",
                table: "Permissions");

            migrationBuilder.DropIndex(
                name: "IX_Permissions_EmployeeId",
                table: "Permissions");

            migrationBuilder.DropColumn(
                name: "TotalDaysOff",
                table: "Permissions");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PermissionId",
                table: "Employees",
                column: "PermissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Permissions_PermissionId",
                table: "Employees",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id");
        }
    }
}

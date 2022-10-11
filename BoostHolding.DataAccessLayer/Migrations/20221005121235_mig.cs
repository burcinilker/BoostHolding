using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoostHolding.DataAccessLayer.Migrations
{
    public partial class mig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeExpenditure");

            migrationBuilder.DropColumn(
                name: "ExpenditureType",
                table: "Expenditures");

            migrationBuilder.AddColumn<int>(
                name: "ExpenditureTypeId",
                table: "Expenditures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExpenditureTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenditureTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenditures_EmployeeId",
                table: "Expenditures",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Expenditures_ExpenditureTypeId",
                table: "Expenditures",
                column: "ExpenditureTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenditures_Employees_EmployeeId",
                table: "Expenditures",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenditures_ExpenditureTypes_ExpenditureTypeId",
                table: "Expenditures",
                column: "ExpenditureTypeId",
                principalTable: "ExpenditureTypes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenditures_Employees_EmployeeId",
                table: "Expenditures");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenditures_ExpenditureTypes_ExpenditureTypeId",
                table: "Expenditures");

            migrationBuilder.DropTable(
                name: "ExpenditureTypes");

            migrationBuilder.DropIndex(
                name: "IX_Expenditures_EmployeeId",
                table: "Expenditures");

            migrationBuilder.DropIndex(
                name: "IX_Expenditures_ExpenditureTypeId",
                table: "Expenditures");

            migrationBuilder.DropColumn(
                name: "ExpenditureTypeId",
                table: "Expenditures");

            migrationBuilder.AddColumn<int>(
                name: "ExpenditureType",
                table: "Expenditures",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeExpenditure_ExpendituresId",
                table: "EmployeeExpenditure",
                column: "ExpendituresId");
        }
    }
}

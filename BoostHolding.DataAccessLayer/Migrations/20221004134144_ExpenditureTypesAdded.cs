using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoostHolding.DataAccessLayer.Migrations
{
    public partial class ExpenditureTypesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_Expenditures_ExpenditureTypeId",
                table: "Expenditures",
                column: "ExpenditureTypeId");

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
                name: "FK_Expenditures_ExpenditureTypes_ExpenditureTypeId",
                table: "Expenditures");

            migrationBuilder.DropTable(
                name: "ExpenditureTypes");

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
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoostHolding.DataAccessLayer.Migrations
{
    public partial class secondadvance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "AdvancePayments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Salary",
                table: "AdvancePayments");
        }
    }
}

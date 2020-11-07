using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class Barangay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaranagyCode",
                table: "Addresses");

            migrationBuilder.AddColumn<string>(
                name: "BarangayCode",
                table: "Addresses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BarangayCode",
                table: "Addresses");

            migrationBuilder.AddColumn<string>(
                name: "BaranagyCode",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

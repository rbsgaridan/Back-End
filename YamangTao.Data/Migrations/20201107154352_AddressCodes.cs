using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class AddressCodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BaranagyCode",
                table: "Addresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MunicipalityCode",
                table: "Addresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceCode",
                table: "Addresses",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegionCode",
                table: "Addresses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaranagyCode",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "MunicipalityCode",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ProvinceCode",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "RegionCode",
                table: "Addresses");
        }
    }
}

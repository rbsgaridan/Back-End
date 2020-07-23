using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class ModIpcrKpi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "MaxWeight",
                table: "KPIs",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MinWeight",
                table: "KPIs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeIdLocation",
                table: "Ipcr",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Ipcr",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxWeight",
                table: "KPIs");

            migrationBuilder.DropColumn(
                name: "MinWeight",
                table: "KPIs");

            migrationBuilder.DropColumn(
                name: "EmployeeIdLocation",
                table: "Ipcr");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Ipcr");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class AddMinIndicators : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinIndicators",
                table: "KpiTemplates",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinIndicators",
                table: "KPIs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Selected",
                table: "KPIs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TotalIndicators",
                table: "KPIs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinIndicators",
                table: "KpiTemplates");

            migrationBuilder.DropColumn(
                name: "MinIndicators",
                table: "KPIs");

            migrationBuilder.DropColumn(
                name: "Selected",
                table: "KPIs");

            migrationBuilder.DropColumn(
                name: "TotalIndicators",
                table: "KPIs");
        }
    }
}

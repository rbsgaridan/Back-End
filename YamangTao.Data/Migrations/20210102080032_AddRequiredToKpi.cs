using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class AddRequiredToKpi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Required",
                table: "KpiTemplates",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Required",
                table: "KPIs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Required",
                table: "KpiTemplates");

            migrationBuilder.DropColumn(
                name: "Required",
                table: "KPIs");
        }
    }
}

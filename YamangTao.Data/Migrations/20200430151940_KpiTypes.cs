using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class KpiTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KpiTypeId",
                table: "KPIs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "KpiTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KPIs_KpiTypeId",
                table: "KPIs",
                column: "KpiTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_KPIs_KpiTypes_KpiTypeId",
                table: "KPIs",
                column: "KpiTypeId",
                principalTable: "KpiTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KPIs_KpiTypes_KpiTypeId",
                table: "KPIs");

            migrationBuilder.DropTable(
                name: "KpiTypes");

            migrationBuilder.DropIndex(
                name: "IX_KPIs_KpiTypeId",
                table: "KPIs");

            migrationBuilder.DropColumn(
                name: "KpiTypeId",
                table: "KPIs");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class modIPCRTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KpiTemplates_KpiTemplates_ParentKpiId",
                table: "KpiTemplates");

            migrationBuilder.AddColumn<string>(
                name: "JobPosition",
                table: "IpcrTemplates",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrgUnit",
                table: "IpcrTemplates",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KpiTemplates_KpiTemplates_ParentKpiId",
                table: "KpiTemplates",
                column: "ParentKpiId",
                principalTable: "KpiTemplates",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KpiTemplates_KpiTemplates_ParentKpiId",
                table: "KpiTemplates");

            migrationBuilder.DropColumn(
                name: "JobPosition",
                table: "IpcrTemplates");

            migrationBuilder.DropColumn(
                name: "OrgUnit",
                table: "IpcrTemplates");

            migrationBuilder.AddForeignKey(
                name: "FK_KpiTemplates_KpiTemplates_ParentKpiId",
                table: "KpiTemplates",
                column: "ParentKpiId",
                principalTable: "KpiTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

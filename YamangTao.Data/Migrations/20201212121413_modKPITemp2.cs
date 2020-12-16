using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class modKPITemp2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KpiTemplates_IpcrTemplates_IpcrTemplateId",
                table: "KpiTemplates");

            migrationBuilder.DropIndex(
                name: "IX_KpiTemplates_IpcrTemplateId",
                table: "KpiTemplates");

            migrationBuilder.AddColumn<int>(
                name: "ParentKpiId",
                table: "KpiTemplates",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_KpiTemplates_ParentKpiId",
                table: "KpiTemplates",
                column: "ParentKpiId");

            migrationBuilder.AddForeignKey(
                name: "FK_KpiTemplates_KpiTemplates_ParentKpiId",
                table: "KpiTemplates",
                column: "ParentKpiId",
                principalTable: "KpiTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KpiTemplates_KpiTemplates_ParentKpiId",
                table: "KpiTemplates");

            migrationBuilder.DropIndex(
                name: "IX_KpiTemplates_ParentKpiId",
                table: "KpiTemplates");

            migrationBuilder.DropColumn(
                name: "ParentKpiId",
                table: "KpiTemplates");

            migrationBuilder.CreateIndex(
                name: "IX_KpiTemplates_IpcrTemplateId",
                table: "KpiTemplates",
                column: "IpcrTemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_KpiTemplates_IpcrTemplates_IpcrTemplateId",
                table: "KpiTemplates",
                column: "IpcrTemplateId",
                principalTable: "IpcrTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

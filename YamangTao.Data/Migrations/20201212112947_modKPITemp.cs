using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class modKPITemp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KpiTemplates_KpiTemplates_ParentKpiId",
                table: "KpiTemplates");

            migrationBuilder.DropIndex(
                name: "IX_KpiTemplates_ParentKpiId",
                table: "KpiTemplates");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "KpiTemplates");

            migrationBuilder.DropColumn(
                name: "IpcrOwnerId",
                table: "KpiTemplates");

            migrationBuilder.DropColumn(
                name: "ParentKpiId",
                table: "KpiTemplates");

            migrationBuilder.AlterColumn<HierarchyId>(
                name: "Path",
                table: "KpiTemplates",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "KpiTemplates",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(HierarchyId),
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "KpiTemplates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IpcrOwnerId",
                table: "KpiTemplates",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParentKpiId",
                table: "KpiTemplates",
                type: "int",
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
                principalColumn: "Id");
        }
    }
}

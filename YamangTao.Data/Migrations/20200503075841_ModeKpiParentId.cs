using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class ModeKpiParentId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KPIs_KPIs_ParentKpiId",
                table: "KPIs");

            migrationBuilder.AlterColumn<int>(
                name: "ParentKpiId",
                table: "KPIs",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_KPIs_KPIs_ParentKpiId",
                table: "KPIs",
                column: "ParentKpiId",
                principalTable: "KPIs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KPIs_KPIs_ParentKpiId",
                table: "KPIs");

            migrationBuilder.AlterColumn<int>(
                name: "ParentKpiId",
                table: "KPIs",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KPIs_KPIs_ParentKpiId",
                table: "KPIs",
                column: "ParentKpiId",
                principalTable: "KPIs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

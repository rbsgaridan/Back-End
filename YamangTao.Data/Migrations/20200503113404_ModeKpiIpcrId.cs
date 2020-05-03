using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class ModeKpiIpcrId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KPIs_Ipcrs_IpcrId",
                table: "KPIs");

            migrationBuilder.AlterColumn<int>(
                name: "IpcrId",
                table: "KPIs",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_KPIs_Ipcrs_IpcrId",
                table: "KPIs",
                column: "IpcrId",
                principalTable: "Ipcrs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KPIs_Ipcrs_IpcrId",
                table: "KPIs");

            migrationBuilder.AlterColumn<int>(
                name: "IpcrId",
                table: "KPIs",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KPIs_Ipcrs_IpcrId",
                table: "KPIs",
                column: "IpcrId",
                principalTable: "Ipcrs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

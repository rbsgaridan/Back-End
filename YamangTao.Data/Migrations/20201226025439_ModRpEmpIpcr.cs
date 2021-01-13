using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class ModRpEmpIpcr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ipcrs_Employees_EmployeeId",
                table: "Ipcrs");

            migrationBuilder.AddForeignKey(
                name: "FK_Ipcrs_Employees_EmployeeId",
                table: "Ipcrs",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ipcrs_Employees_EmployeeId",
                table: "Ipcrs");

            migrationBuilder.AddForeignKey(
                name: "FK_Ipcrs_Employees_EmployeeId",
                table: "Ipcrs",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

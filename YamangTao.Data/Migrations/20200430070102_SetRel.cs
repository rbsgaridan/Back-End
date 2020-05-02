using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class SetRel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_OrgUnits_OrgUnitId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_OrgUnits_OrgUnits_ParentUnitId",
                table: "OrgUnits");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_OrgUnits_OrgUnitId",
                table: "Employees",
                column: "OrgUnitId",
                principalTable: "OrgUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrgUnits_OrgUnits_ParentUnitId",
                table: "OrgUnits",
                column: "ParentUnitId",
                principalTable: "OrgUnits",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_OrgUnits_OrgUnitId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_OrgUnits_OrgUnits_ParentUnitId",
                table: "OrgUnits");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_OrgUnits_OrgUnitId",
                table: "Employees",
                column: "OrgUnitId",
                principalTable: "OrgUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrgUnits_OrgUnits_ParentUnitId",
                table: "OrgUnits",
                column: "ParentUnitId",
                principalTable: "OrgUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

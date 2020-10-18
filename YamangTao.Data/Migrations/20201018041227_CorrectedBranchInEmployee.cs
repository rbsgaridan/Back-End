using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class CorrectedBranchInEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BranchCampuses_CurrentCampusId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_CurrentCampusId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BrachCampusId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CurrentCampusId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "BranchCampusId",
                table: "Employees",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_BranchCampusId",
                table: "Employees",
                column: "BranchCampusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BranchCampuses_BranchCampusId",
                table: "Employees",
                column: "BranchCampusId",
                principalTable: "BranchCampuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BranchCampuses_BranchCampusId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_BranchCampusId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BranchCampusId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "BrachCampusId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentCampusId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CurrentCampusId",
                table: "Employees",
                column: "CurrentCampusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_BranchCampuses_CurrentCampusId",
                table: "Employees",
                column: "CurrentCampusId",
                principalTable: "BranchCampuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

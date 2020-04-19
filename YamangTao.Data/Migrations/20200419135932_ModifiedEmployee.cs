using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class ModifiedEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrachCampusId",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentCampusId",
                table: "Employees",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BranchCampuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Campus = table.Column<string>(maxLength: 100, nullable: true),
                    Address = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchCampuses", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_BranchCampuses_CurrentCampusId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "BranchCampuses");

            migrationBuilder.DropIndex(
                name: "IX_Employees_CurrentCampusId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "BrachCampusId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CurrentCampusId",
                table: "Employees");
        }
    }
}

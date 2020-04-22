using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class AddedOrgUnits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrgUnits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UnitName = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    UnitType = table.Column<string>(nullable: true),
                    NameOfHead = table.Column<string>(nullable: true),
                    CurrentHeadId = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    ParentUnitId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrgUnits_Employees_CurrentHeadId",
                        column: x => x.CurrentHeadId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrgUnits_OrgUnits_ParentUnitId",
                        column: x => x.ParentUnitId,
                        principalTable: "OrgUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrgUnits_CurrentHeadId",
                table: "OrgUnits",
                column: "CurrentHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgUnits_ParentUnitId",
                table: "OrgUnits",
                column: "ParentUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrgUnits");
        }
    }
}

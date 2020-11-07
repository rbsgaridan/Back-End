using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class CascadeDeletePds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_PersonalDataSheets_PdsId",
                table: "Addresses");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_PersonalDataSheets_PdsId",
                table: "Addresses",
                column: "PdsId",
                principalTable: "PersonalDataSheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_PersonalDataSheets_PdsId",
                table: "Addresses");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_PersonalDataSheets_PdsId",
                table: "Addresses",
                column: "PdsId",
                principalTable: "PersonalDataSheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

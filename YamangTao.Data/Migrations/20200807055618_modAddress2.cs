using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class modAddress2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_PersonalDataSheets_PdsId",
                table: "Addresses");

            migrationBuilder.AlterColumn<int>(
                name: "PdsId",
                table: "Addresses",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_PersonalDataSheets_PdsId",
                table: "Addresses",
                column: "PdsId",
                principalTable: "PersonalDataSheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_PersonalDataSheets_PdsId",
                table: "Addresses");

            migrationBuilder.AlterColumn<int>(
                name: "PdsId",
                table: "Addresses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_PersonalDataSheets_PdsId",
                table: "Addresses",
                column: "PdsId",
                principalTable: "PersonalDataSheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

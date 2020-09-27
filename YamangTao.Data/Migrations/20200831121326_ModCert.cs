using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class ModCert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Certificate_ActivityId",
                table: "Certificate",
                column: "ActivityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certificate_Activities_ActivityId",
                table: "Certificate",
                column: "ActivityId",
                principalTable: "Activities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certificate_Activities_ActivityId",
                table: "Certificate");

            migrationBuilder.DropIndex(
                name: "IX_Certificate_ActivityId",
                table: "Certificate");
        }
    }
}

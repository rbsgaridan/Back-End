using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class RatingPerio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PeriodFrom",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "PeriodTo",
                table: "Ipcrs");

            migrationBuilder.AddColumn<int>(
                name: "RatingPeriodId",
                table: "Ipcrs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RatingPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 256, nullable: true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingPeriods", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ipcrs_RatingPeriodId",
                table: "Ipcrs",
                column: "RatingPeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ipcrs_RatingPeriods_RatingPeriodId",
                table: "Ipcrs",
                column: "RatingPeriodId",
                principalTable: "RatingPeriods",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ipcrs_RatingPeriods_RatingPeriodId",
                table: "Ipcrs");

            migrationBuilder.DropTable(
                name: "RatingPeriods");

            migrationBuilder.DropIndex(
                name: "IX_Ipcrs_RatingPeriodId",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "RatingPeriodId",
                table: "Ipcrs");

            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodFrom",
                table: "Ipcrs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PeriodTo",
                table: "Ipcrs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class modActandTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CertificateDate",
                table: "Certificate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EventDuration",
                table: "Certificate",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "Activities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Start",
                table: "Activities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CertificateDate",
                table: "Certificate");

            migrationBuilder.DropColumn(
                name: "EventDuration",
                table: "Certificate");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "End",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Activities");
        }
    }
}

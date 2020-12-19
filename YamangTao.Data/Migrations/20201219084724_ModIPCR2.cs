using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class ModIPCR2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ipcrs_Employees_CompiledById",
                table: "Ipcrs");

            migrationBuilder.DropIndex(
                name: "IX_Ipcrs_CompiledById",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "CompiledById",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "CompilerDesignation",
                table: "Ipcrs");

            migrationBuilder.AddColumn<string>(
                name: "AccompAssessedBy",
                table: "Ipcrs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccompAssessorDesignation",
                table: "Ipcrs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccomptAssessedById",
                table: "Ipcrs",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AccomptDateAssessed",
                table: "Ipcrs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TargetAssessedBy",
                table: "Ipcrs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TargetAssessedById",
                table: "Ipcrs",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TargetAssessorDesignation",
                table: "Ipcrs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TargetDateAssessed",
                table: "Ipcrs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccompAssessedBy",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "AccompAssessorDesignation",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "AccomptAssessedById",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "AccomptDateAssessed",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "TargetAssessedBy",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "TargetAssessedById",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "TargetAssessorDesignation",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "TargetDateAssessed",
                table: "Ipcrs");

            migrationBuilder.AddColumn<string>(
                name: "CompiledById",
                table: "Ipcrs",
                type: "nvarchar(30)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompilerDesignation",
                table: "Ipcrs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ipcrs_CompiledById",
                table: "Ipcrs",
                column: "CompiledById");

            migrationBuilder.AddForeignKey(
                name: "FK_Ipcrs_Employees_CompiledById",
                table: "Ipcrs",
                column: "CompiledById",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

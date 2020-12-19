using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class ModIPCR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ipcrs_Employees_ApprovedById",
                table: "Ipcrs");

            migrationBuilder.DropForeignKey(
                name: "FK_Ipcrs_Employees_ReviewedById",
                table: "Ipcrs");

            migrationBuilder.DropIndex(
                name: "IX_Ipcrs_ApprovedById",
                table: "Ipcrs");

            migrationBuilder.DropIndex(
                name: "IX_Ipcrs_ReviewedById",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "ApprovedById",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "ApproverDesignation",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "DateApproved",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "DateReviewed",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "ReviewedById",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "ReviewerDesignation",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "isLocked",
                table: "Ipcrs");

            migrationBuilder.AddColumn<string>(
                name: "AccompApprovedBy",
                table: "Ipcrs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccompApprovedById",
                table: "Ipcrs",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccompApproverDesignation",
                table: "Ipcrs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AccompDateApproved",
                table: "Ipcrs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccompReviewedBy",
                table: "Ipcrs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccompReviewedById",
                table: "Ipcrs",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccompReviewerDesignation",
                table: "Ipcrs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AccomptDateReviewed",
                table: "Ipcrs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TargetApprovedBy",
                table: "Ipcrs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TargetApprovedById",
                table: "Ipcrs",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TargetApproverDesignation",
                table: "Ipcrs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TargetDateApproved",
                table: "Ipcrs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TargetDateReviewed",
                table: "Ipcrs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TargetReviewedBy",
                table: "Ipcrs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TargetReviewedById",
                table: "Ipcrs",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TargetReviewerDesignation",
                table: "Ipcrs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isAccomplishmentLocked",
                table: "Ipcrs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isTargetLocked",
                table: "Ipcrs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "DocumentPaths",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccompApprovedBy",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "AccompApprovedById",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "AccompApproverDesignation",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "AccompDateApproved",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "AccompReviewedBy",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "AccompReviewedById",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "AccompReviewerDesignation",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "AccomptDateReviewed",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "TargetApprovedBy",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "TargetApprovedById",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "TargetApproverDesignation",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "TargetDateApproved",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "TargetDateReviewed",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "TargetReviewedBy",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "TargetReviewedById",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "TargetReviewerDesignation",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "isAccomplishmentLocked",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "isTargetLocked",
                table: "Ipcrs");

            migrationBuilder.DropColumn(
                name: "Designation",
                table: "DocumentPaths");

            migrationBuilder.AddColumn<string>(
                name: "ApprovedById",
                table: "Ipcrs",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApproverDesignation",
                table: "Ipcrs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateApproved",
                table: "Ipcrs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateReviewed",
                table: "Ipcrs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReviewedById",
                table: "Ipcrs",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReviewerDesignation",
                table: "Ipcrs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isLocked",
                table: "Ipcrs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Ipcrs_ApprovedById",
                table: "Ipcrs",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_Ipcrs_ReviewedById",
                table: "Ipcrs",
                column: "ReviewedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Ipcrs_Employees_ApprovedById",
                table: "Ipcrs",
                column: "ApprovedById",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ipcrs_Employees_ReviewedById",
                table: "Ipcrs",
                column: "ReviewedById",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

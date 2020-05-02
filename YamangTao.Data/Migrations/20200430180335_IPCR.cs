using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class IPCR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IpcrId",
                table: "KPIs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Ipcrs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsTemplate = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 30, nullable: true),
                    JobPositionId = table.Column<int>(nullable: false),
                    OrgUnitId = table.Column<int>(nullable: false),
                    DateTargetApproved = table.Column<DateTime>(nullable: false),
                    CompiledById = table.Column<string>(maxLength: 30, nullable: true),
                    CompilerDesignation = table.Column<string>(nullable: true),
                    PeriodFrom = table.Column<DateTime>(nullable: false),
                    PeriodTo = table.Column<DateTime>(nullable: false),
                    FinalQrating = table.Column<double>(nullable: false),
                    FinalErating = table.Column<double>(nullable: false),
                    FinalTrating = table.Column<double>(nullable: false),
                    FinalAverageRating = table.Column<double>(nullable: false),
                    AdjectivalRating = table.Column<string>(nullable: true),
                    ReviewedById = table.Column<string>(maxLength: 30, nullable: true),
                    ReviewerDesignation = table.Column<string>(nullable: true),
                    DateReviewed = table.Column<DateTime>(nullable: false),
                    ApprovedById = table.Column<string>(maxLength: 30, nullable: true),
                    ApproverDesignation = table.Column<string>(nullable: true),
                    DateApproved = table.Column<DateTime>(nullable: false),
                    LandDRecommendation = table.Column<string>(nullable: true),
                    RnRRecommendation = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateLastModified = table.Column<DateTime>(nullable: true),
                    DateLastPrinted = table.Column<DateTime>(nullable: true),
                    Reviewed = table.Column<bool>(nullable: false),
                    Compiled = table.Column<bool>(nullable: false),
                    Approved = table.Column<bool>(nullable: false),
                    isLocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ipcrs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ipcrs_Employees_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ipcrs_Employees_CompiledById",
                        column: x => x.CompiledById,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ipcrs_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ipcrs_JobPositions_JobPositionId",
                        column: x => x.JobPositionId,
                        principalTable: "JobPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ipcrs_OrgUnits_OrgUnitId",
                        column: x => x.OrgUnitId,
                        principalTable: "OrgUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ipcrs_Employees_ReviewedById",
                        column: x => x.ReviewedById,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KPIs_IpcrId",
                table: "KPIs",
                column: "IpcrId");

            migrationBuilder.CreateIndex(
                name: "IX_Ipcrs_ApprovedById",
                table: "Ipcrs",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_Ipcrs_CompiledById",
                table: "Ipcrs",
                column: "CompiledById");

            migrationBuilder.CreateIndex(
                name: "IX_Ipcrs_EmployeeId",
                table: "Ipcrs",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ipcrs_JobPositionId",
                table: "Ipcrs",
                column: "JobPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Ipcrs_OrgUnitId",
                table: "Ipcrs",
                column: "OrgUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Ipcrs_ReviewedById",
                table: "Ipcrs",
                column: "ReviewedById");

            migrationBuilder.AddForeignKey(
                name: "FK_KPIs_Ipcrs_IpcrId",
                table: "KPIs",
                column: "IpcrId",
                principalTable: "Ipcrs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KPIs_Ipcrs_IpcrId",
                table: "KPIs");

            migrationBuilder.DropTable(
                name: "Ipcrs");

            migrationBuilder.DropIndex(
                name: "IX_KPIs_IpcrId",
                table: "KPIs");

            migrationBuilder.DropColumn(
                name: "IpcrId",
                table: "KPIs");
        }
    }
}

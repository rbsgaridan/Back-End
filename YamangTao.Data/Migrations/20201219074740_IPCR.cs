using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class IPCR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ipcrs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(maxLength: 30, nullable: true),
                    JobPositionId = table.Column<int>(nullable: false),
                    OrgUnitId = table.Column<int>(nullable: false),
                    DateTargetApproved = table.Column<DateTime>(nullable: true),
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
                    DateReviewed = table.Column<DateTime>(nullable: true),
                    ApprovedById = table.Column<string>(maxLength: 30, nullable: true),
                    ApproverDesignation = table.Column<string>(nullable: true),
                    DateApproved = table.Column<DateTime>(nullable: true),
                    LandDRecommendation = table.Column<string>(nullable: true),
                    RnRRecommendation = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateLastModified = table.Column<DateTime>(nullable: true),
                    DateLastPrinted = table.Column<DateTime>(nullable: true),
                    Reviewed = table.Column<bool>(nullable: false),
                    Compiled = table.Column<bool>(nullable: false),
                    Approved = table.Column<bool>(nullable: false),
                    isLocked = table.Column<bool>(nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: true),
                    EmployeeIdLocation = table.Column<string>(maxLength: 30, nullable: true),
                    PreviousHolder = table.Column<string>(maxLength: 50, nullable: true),
                    CurrentHolder = table.Column<string>(maxLength: 50, nullable: true),
                    NextUser = table.Column<string>(maxLength: 50, nullable: true)
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

            migrationBuilder.CreateTable(
                name: "KPIs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    OrderNumber = table.Column<string>(maxLength: 10, nullable: true),
                    mfoOO = table.Column<string>(nullable: true),
                    IpcrId = table.Column<int>(nullable: true),
                    KpiTypeId = table.Column<int>(nullable: false),
                    Weight = table.Column<float>(nullable: false),
                    SuccessIndicator = table.Column<string>(maxLength: 256, nullable: true),
                    ActualAccomplishment = table.Column<string>(maxLength: 256, nullable: true),
                    HasQuality = table.Column<bool>(nullable: false),
                    HasEfficiency = table.Column<bool>(nullable: false),
                    HasTimeliness = table.Column<bool>(nullable: false),
                    QualityRating = table.Column<float>(nullable: false),
                    EfficiencyRating = table.Column<float>(nullable: false),
                    TimelinessRating = table.Column<float>(nullable: false),
                    AverageRating = table.Column<float>(nullable: false),
                    TaskId = table.Column<string>(nullable: true),
                    ParentKpiId = table.Column<int>(nullable: true),
                    MaxWeight = table.Column<float>(nullable: true),
                    MinWeight = table.Column<float>(nullable: true),
                    IpcrOwnerId = table.Column<int>(nullable: true),
                    EmployeeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPIs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KPIs_KpiTypes_KpiTypeId",
                        column: x => x.KpiTypeId,
                        principalTable: "KpiTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KPIs_KPIs_ParentKpiId",
                        column: x => x.ParentKpiId,
                        principalTable: "KPIs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RatingMatrix",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KpiId = table.Column<int>(nullable: true),
                    Dimension = table.Column<string>(maxLength: 15, nullable: true),
                    MeansOfVerification = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingMatrix", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingMatrix_KPIs_KpiId",
                        column: x => x.KpiId,
                        principalTable: "KPIs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    RatingMatrixId = table.Column<long>(nullable: false),
                    Rate = table.Column<short>(nullable: false),
                    Id = table.Column<long>(nullable: false),
                    Description = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => new { x.RatingMatrixId, x.Rate });
                    table.ForeignKey(
                        name: "FK_Ratings_RatingMatrix_RatingMatrixId",
                        column: x => x.RatingMatrixId,
                        principalTable: "RatingMatrix",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_KPIs_KpiTypeId",
                table: "KPIs",
                column: "KpiTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_KPIs_ParentKpiId",
                table: "KPIs",
                column: "ParentKpiId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingMatrix_KpiId",
                table: "RatingMatrix",
                column: "KpiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ipcrs");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "RatingMatrix");

            migrationBuilder.DropTable(
                name: "KPIs");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class temp_rem_IPCR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "RatingMatrix");

            migrationBuilder.DropTable(
                name: "Kpi");

            migrationBuilder.DropTable(
                name: "Ipcr");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ipcr",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdjectivalRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedById = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    ApproverDesignation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Compiled = table.Column<bool>(type: "bit", nullable: false),
                    CompiledById = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    CompilerDesignation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentHolder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateApproved = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateLastPrinted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateReviewed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateTargetApproved = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmployeeId = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    EmployeeIdLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FinalAverageRating = table.Column<double>(type: "float", nullable: false),
                    FinalErating = table.Column<double>(type: "float", nullable: false),
                    FinalQrating = table.Column<double>(type: "float", nullable: false),
                    FinalTrating = table.Column<double>(type: "float", nullable: false),
                    IsTemplate = table.Column<bool>(type: "bit", nullable: false),
                    JobPositionId = table.Column<int>(type: "int", nullable: false),
                    LandDRecommendation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrgUnitId = table.Column<int>(type: "int", nullable: false),
                    PeriodFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PeriodTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PreviousHolder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reviewed = table.Column<bool>(type: "bit", nullable: false),
                    ReviewedById = table.Column<string>(type: "nvarchar(30)", nullable: true),
                    ReviewerDesignation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RnRRecommendation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isLocked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ipcr", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ipcr_Employees_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ipcr_Employees_CompiledById",
                        column: x => x.CompiledById,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ipcr_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ipcr_JobPositions_JobPositionId",
                        column: x => x.JobPositionId,
                        principalTable: "JobPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ipcr_OrgUnits_OrgUnitId",
                        column: x => x.OrgUnitId,
                        principalTable: "OrgUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ipcr_Employees_ReviewedById",
                        column: x => x.ReviewedById,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kpi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActualAccomplishment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AverageRating = table.Column<float>(type: "real", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EfficiencyRating = table.Column<float>(type: "real", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HasEfficiency = table.Column<bool>(type: "bit", nullable: false),
                    HasQuality = table.Column<bool>(type: "bit", nullable: false),
                    HasTimeliness = table.Column<bool>(type: "bit", nullable: false),
                    IpcrId = table.Column<int>(type: "int", nullable: true),
                    IpcrOwnerId = table.Column<int>(type: "int", nullable: true),
                    KpiTypeId = table.Column<int>(type: "int", nullable: false),
                    MaxWeight = table.Column<float>(type: "real", nullable: true),
                    MinWeight = table.Column<float>(type: "real", nullable: true),
                    OrderNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentKpiId = table.Column<int>(type: "int", nullable: true),
                    QualityRating = table.Column<float>(type: "real", nullable: false),
                    SuccessIndicator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimelinessRating = table.Column<float>(type: "real", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    mfoOO = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kpi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kpi_Ipcr_IpcrId",
                        column: x => x.IpcrId,
                        principalTable: "Ipcr",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kpi_KpiTypes_KpiTypeId",
                        column: x => x.KpiTypeId,
                        principalTable: "KpiTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Kpi_Kpi_ParentKpiId",
                        column: x => x.ParentKpiId,
                        principalTable: "Kpi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RatingMatrix",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dimension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KpiId = table.Column<int>(type: "int", nullable: true),
                    MeansOfVerification = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingMatrix", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingMatrix_Kpi_KpiId",
                        column: x => x.KpiId,
                        principalTable: "Kpi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<short>(type: "smallint", nullable: false),
                    RatingMatrixId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rating_RatingMatrix_RatingMatrixId",
                        column: x => x.RatingMatrixId,
                        principalTable: "RatingMatrix",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ipcr_ApprovedById",
                table: "Ipcr",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_Ipcr_CompiledById",
                table: "Ipcr",
                column: "CompiledById");

            migrationBuilder.CreateIndex(
                name: "IX_Ipcr_EmployeeId",
                table: "Ipcr",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Ipcr_JobPositionId",
                table: "Ipcr",
                column: "JobPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Ipcr_OrgUnitId",
                table: "Ipcr",
                column: "OrgUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Ipcr_ReviewedById",
                table: "Ipcr",
                column: "ReviewedById");

            migrationBuilder.CreateIndex(
                name: "IX_Kpi_IpcrId",
                table: "Kpi",
                column: "IpcrId");

            migrationBuilder.CreateIndex(
                name: "IX_Kpi_KpiTypeId",
                table: "Kpi",
                column: "KpiTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Kpi_ParentKpiId",
                table: "Kpi",
                column: "ParentKpiId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_RatingMatrixId",
                table: "Rating",
                column: "RatingMatrixId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingMatrix_KpiId",
                table: "RatingMatrix",
                column: "KpiId");
        }
    }
}

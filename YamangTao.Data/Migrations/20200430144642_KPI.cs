using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class KPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PerformanceIndicatorId",
                table: "RatingMatrix");

            migrationBuilder.AddColumn<int>(
                name: "KpiEId",
                table: "RatingMatrix",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KpiQId",
                table: "RatingMatrix",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "KpiTId",
                table: "RatingMatrix",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "KPIs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    OrderNumber = table.Column<string>(maxLength: 10, nullable: true),
                    Weight = table.Column<float>(nullable: false),
                    SuccessIndicator = table.Column<string>(maxLength: 256, nullable: true),
                    ActualAccomplishment = table.Column<string>(maxLength: 256, nullable: true),
                    HasQuality = table.Column<bool>(nullable: false),
                    HasEfficiency = table.Column<bool>(nullable: false),
                    HasTimeliness = table.Column<bool>(nullable: false),
                    QualityRating = table.Column<int>(nullable: false),
                    EfficiencyRating = table.Column<int>(nullable: false),
                    TimelinessRating = table.Column<int>(nullable: false),
                    TaskId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPIs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RatingMatrix_KpiEId",
                table: "RatingMatrix",
                column: "KpiEId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingMatrix_KpiQId",
                table: "RatingMatrix",
                column: "KpiQId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingMatrix_KpiTId",
                table: "RatingMatrix",
                column: "KpiTId");

            migrationBuilder.AddForeignKey(
                name: "FK_RatingMatrix_KPIs_KpiEId",
                table: "RatingMatrix",
                column: "KpiEId",
                principalTable: "KPIs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingMatrix_KPIs_KpiQId",
                table: "RatingMatrix",
                column: "KpiQId",
                principalTable: "KPIs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingMatrix_KPIs_KpiTId",
                table: "RatingMatrix",
                column: "KpiTId",
                principalTable: "KPIs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RatingMatrix_KPIs_KpiEId",
                table: "RatingMatrix");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingMatrix_KPIs_KpiQId",
                table: "RatingMatrix");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingMatrix_KPIs_KpiTId",
                table: "RatingMatrix");

            migrationBuilder.DropTable(
                name: "KPIs");

            migrationBuilder.DropIndex(
                name: "IX_RatingMatrix_KpiEId",
                table: "RatingMatrix");

            migrationBuilder.DropIndex(
                name: "IX_RatingMatrix_KpiQId",
                table: "RatingMatrix");

            migrationBuilder.DropIndex(
                name: "IX_RatingMatrix_KpiTId",
                table: "RatingMatrix");

            migrationBuilder.DropColumn(
                name: "KpiEId",
                table: "RatingMatrix");

            migrationBuilder.DropColumn(
                name: "KpiQId",
                table: "RatingMatrix");

            migrationBuilder.DropColumn(
                name: "KpiTId",
                table: "RatingMatrix");

            migrationBuilder.AddColumn<int>(
                name: "PerformanceIndicatorId",
                table: "RatingMatrix",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

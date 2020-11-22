using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class IPCR_Templates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KPIs_Ipcr_IpcrId",
                table: "KPIs");

            migrationBuilder.DropForeignKey(
                name: "FK_KPIs_KpiTypes_KpiTypeId",
                table: "KPIs");

            migrationBuilder.DropForeignKey(
                name: "FK_KPIs_KPIs_ParentKpiId",
                table: "KPIs");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingMatrix_KPIs_KpiId",
                table: "RatingMatrix");

            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_RatingMatrix_RatingMatrixId",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KPIs",
                table: "KPIs");

            migrationBuilder.RenameTable(
                name: "Ratings",
                newName: "Rating");

            migrationBuilder.RenameTable(
                name: "KPIs",
                newName: "Kpi");

            migrationBuilder.RenameIndex(
                name: "IX_KPIs_ParentKpiId",
                table: "Kpi",
                newName: "IX_Kpi_ParentKpiId");

            migrationBuilder.RenameIndex(
                name: "IX_KPIs_KpiTypeId",
                table: "Kpi",
                newName: "IX_Kpi_KpiTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_KPIs_IpcrId",
                table: "Kpi",
                newName: "IX_Kpi_IpcrId");

            migrationBuilder.AlterColumn<string>(
                name: "MeansOfVerification",
                table: "RatingMatrix",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Dimension",
                table: "RatingMatrix",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Ipcr",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeIdLocation",
                table: "Ipcr",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Rating",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "Rating",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "SuccessIndicator",
                table: "Kpi",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                table: "Kpi",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Kpi",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActualAccomplishment",
                table: "Kpi",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rating",
                table: "Rating",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Kpi",
                table: "Kpi",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "IpcrTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 50, nullable: true),
                    JobPositionId = table.Column<int>(nullable: false),
                    OrgUnitId = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateLastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpcrTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KpiTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: true),
                    OrderNumber = table.Column<string>(nullable: true),
                    mfoOO = table.Column<string>(nullable: true),
                    Path = table.Column<string>(maxLength: 100, nullable: true),
                    IpcrTemplateId = table.Column<int>(nullable: true),
                    KpiTypeId = table.Column<int>(nullable: false),
                    SuccessIndicator = table.Column<string>(maxLength: 500, nullable: true),
                    HasQuality = table.Column<bool>(nullable: false),
                    HasEfficiency = table.Column<bool>(nullable: false),
                    HasTimeliness = table.Column<bool>(nullable: false),
                    AverageRating = table.Column<float>(nullable: false),
                    TaskId = table.Column<string>(maxLength: 50, nullable: true),
                    ParentKpiId = table.Column<int>(nullable: true),
                    MaxWeight = table.Column<float>(nullable: true),
                    MinWeight = table.Column<float>(nullable: true),
                    IpcrOwnerId = table.Column<int>(nullable: true),
                    EmployeeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KpiTemplates_IpcrTemplates_IpcrTemplateId",
                        column: x => x.IpcrTemplateId,
                        principalTable: "IpcrTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KpiTemplates_KpiTypes_KpiTypeId",
                        column: x => x.KpiTypeId,
                        principalTable: "KpiTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KpiTemplates_KpiTemplates_ParentKpiId",
                        column: x => x.ParentKpiId,
                        principalTable: "KpiTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RatingMatrixTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KpiId = table.Column<int>(nullable: true),
                    Dimension = table.Column<string>(maxLength: 15, nullable: true),
                    MeansOfVerification = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingMatrixTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingMatrixTemplates_KpiTemplates_KpiId",
                        column: x => x.KpiId,
                        principalTable: "KpiTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingTemplates",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RatingMatrixId = table.Column<int>(nullable: false),
                    Rate = table.Column<short>(nullable: false),
                    Description = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingTemplates_RatingMatrixTemplates_RatingMatrixId",
                        column: x => x.RatingMatrixId,
                        principalTable: "RatingMatrixTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rating_RatingMatrixId",
                table: "Rating",
                column: "RatingMatrixId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiTemplates_IpcrTemplateId",
                table: "KpiTemplates",
                column: "IpcrTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiTemplates_KpiTypeId",
                table: "KpiTemplates",
                column: "KpiTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiTemplates_ParentKpiId",
                table: "KpiTemplates",
                column: "ParentKpiId");

            migrationBuilder.CreateIndex(
                name: "IX_KpiTemplates_Path",
                table: "KpiTemplates",
                column: "Path");

            migrationBuilder.CreateIndex(
                name: "IX_RatingMatrixTemplates_KpiId",
                table: "RatingMatrixTemplates",
                column: "KpiId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingTemplates_RatingMatrixId",
                table: "RatingTemplates",
                column: "RatingMatrixId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kpi_Ipcr_IpcrId",
                table: "Kpi",
                column: "IpcrId",
                principalTable: "Ipcr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Kpi_KpiTypes_KpiTypeId",
                table: "Kpi",
                column: "KpiTypeId",
                principalTable: "KpiTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Kpi_Kpi_ParentKpiId",
                table: "Kpi",
                column: "ParentKpiId",
                principalTable: "Kpi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_RatingMatrix_RatingMatrixId",
                table: "Rating",
                column: "RatingMatrixId",
                principalTable: "RatingMatrix",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingMatrix_Kpi_KpiId",
                table: "RatingMatrix",
                column: "KpiId",
                principalTable: "Kpi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kpi_Ipcr_IpcrId",
                table: "Kpi");

            migrationBuilder.DropForeignKey(
                name: "FK_Kpi_KpiTypes_KpiTypeId",
                table: "Kpi");

            migrationBuilder.DropForeignKey(
                name: "FK_Kpi_Kpi_ParentKpiId",
                table: "Kpi");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_RatingMatrix_RatingMatrixId",
                table: "Rating");

            migrationBuilder.DropForeignKey(
                name: "FK_RatingMatrix_Kpi_KpiId",
                table: "RatingMatrix");

            migrationBuilder.DropTable(
                name: "RatingTemplates");

            migrationBuilder.DropTable(
                name: "RatingMatrixTemplates");

            migrationBuilder.DropTable(
                name: "KpiTemplates");

            migrationBuilder.DropTable(
                name: "IpcrTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rating",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_RatingMatrixId",
                table: "Rating");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Kpi",
                table: "Kpi");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Rating");

            migrationBuilder.RenameTable(
                name: "Rating",
                newName: "Ratings");

            migrationBuilder.RenameTable(
                name: "Kpi",
                newName: "KPIs");

            migrationBuilder.RenameIndex(
                name: "IX_Kpi_ParentKpiId",
                table: "KPIs",
                newName: "IX_KPIs_ParentKpiId");

            migrationBuilder.RenameIndex(
                name: "IX_Kpi_KpiTypeId",
                table: "KPIs",
                newName: "IX_KPIs_KpiTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Kpi_IpcrId",
                table: "KPIs",
                newName: "IX_KPIs_IpcrId");

            migrationBuilder.AlterColumn<string>(
                name: "MeansOfVerification",
                table: "RatingMatrix",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Dimension",
                table: "RatingMatrix",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Ipcr",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeIdLocation",
                table: "Ipcr",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Ratings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SuccessIndicator",
                table: "KPIs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OrderNumber",
                table: "KPIs",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "KPIs",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActualAccomplishment",
                table: "KPIs",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ratings",
                table: "Ratings",
                columns: new[] { "RatingMatrixId", "Rate" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_KPIs",
                table: "KPIs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_KPIs_Ipcr_IpcrId",
                table: "KPIs",
                column: "IpcrId",
                principalTable: "Ipcr",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KPIs_KpiTypes_KpiTypeId",
                table: "KPIs",
                column: "KpiTypeId",
                principalTable: "KpiTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KPIs_KPIs_ParentKpiId",
                table: "KPIs",
                column: "ParentKpiId",
                principalTable: "KPIs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RatingMatrix_KPIs_KpiId",
                table: "RatingMatrix",
                column: "KpiId",
                principalTable: "KPIs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_RatingMatrix_RatingMatrixId",
                table: "Ratings",
                column: "RatingMatrixId",
                principalTable: "RatingMatrix",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class Ratings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RatingMatrix",
                columns: table => new
                {
                    Id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PerformanceIndicatorId = table.Column<int>(nullable: false),
                    Dimension = table.Column<string>(maxLength: 15, nullable: true),
                    MeansOfVerification = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingMatrix", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    id = table.Column<uint>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RatingMatrixId = table.Column<uint>(nullable: false),
                    Rate = table.Column<sbyte>(nullable: false),
                    Desciption = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.id);
                    table.ForeignKey(
                        name: "FK_Ratings_RatingMatrix_RatingMatrixId",
                        column: x => x.RatingMatrixId,
                        principalTable: "RatingMatrix",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_RatingMatrixId",
                table: "Ratings",
                column: "RatingMatrixId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "RatingMatrix");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class RatingsToFloat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "TimelinessRating",
                table: "KPIs",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint unsigned");

            migrationBuilder.AlterColumn<float>(
                name: "QualityRating",
                table: "KPIs",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint unsigned");

            migrationBuilder.AlterColumn<float>(
                name: "EfficiencyRating",
                table: "KPIs",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint unsigned");

            migrationBuilder.AlterColumn<float>(
                name: "AverageRating",
                table: "KPIs",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "TimelinessRating",
                table: "KPIs",
                type: "tinyint unsigned",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<byte>(
                name: "QualityRating",
                table: "KPIs",
                type: "tinyint unsigned",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<byte>(
                name: "EfficiencyRating",
                table: "KPIs",
                type: "tinyint unsigned",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "AverageRating",
                table: "KPIs",
                type: "double",
                nullable: false,
                oldClrType: typeof(float));
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class modKpi2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CurrentHolder",
                table: "PersonalDataSheets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NextUser",
                table: "PersonalDataSheets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousHolder",
                table: "PersonalDataSheets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PersonalDataSheets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "KPIs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IpcrOwnerId",
                table: "KPIs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrentHolder",
                table: "Ipcr",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NextUser",
                table: "Ipcr",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousHolder",
                table: "Ipcr",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DocumentPaths",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentType = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    OrgUnitId = table.Column<int>(nullable: false),
                    OrgUnitName = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<string>(nullable: true),
                    EmployeeName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentPaths", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentPaths");

            migrationBuilder.DropColumn(
                name: "CurrentHolder",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "NextUser",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "PreviousHolder",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "KPIs");

            migrationBuilder.DropColumn(
                name: "IpcrOwnerId",
                table: "KPIs");

            migrationBuilder.DropColumn(
                name: "CurrentHolder",
                table: "Ipcr");

            migrationBuilder.DropColumn(
                name: "NextUser",
                table: "Ipcr");

            migrationBuilder.DropColumn(
                name: "PreviousHolder",
                table: "Ipcr");
        }
    }
}

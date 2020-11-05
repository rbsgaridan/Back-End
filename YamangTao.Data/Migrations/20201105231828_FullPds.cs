using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class FullPds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.RenameColumn(
                name: "IDType",
                table: "Identifications",
                newName: "IdType");

            migrationBuilder.AddColumn<string>(
                name: "AgencyNumber",
                table: "PersonalDataSheets",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BirthPlace",
                table: "PersonalDataSheets",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Citizenship",
                table: "PersonalDataSheets",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DualCitizenCountry",
                table: "PersonalDataSheets",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DualCitizenType",
                table: "PersonalDataSheets",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherFirstname",
                table: "PersonalDataSheets",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherMiddle",
                table: "PersonalDataSheets",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherSuffix",
                table: "PersonalDataSheets",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherSurname",
                table: "PersonalDataSheets",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "PersonalDataSheets",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GsisNumber",
                table: "PersonalDataSheets",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HdmfNumber",
                table: "PersonalDataSheets",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherFirstname",
                table: "PersonalDataSheets",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherMaidenName",
                table: "PersonalDataSheets",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherMiddle",
                table: "PersonalDataSheets",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherSuffix",
                table: "PersonalDataSheets",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherSurname",
                table: "PersonalDataSheets",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherCivilStatus",
                table: "PersonalDataSheets",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhilHealthNumber",
                table: "PersonalDataSheets",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpouseEmployer",
                table: "PersonalDataSheets",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpouseEmployerAddress",
                table: "PersonalDataSheets",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpouseEmployerTelNumber",
                table: "PersonalDataSheets",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpouseFirstname",
                table: "PersonalDataSheets",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpouseMiddle",
                table: "PersonalDataSheets",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpouseOccupation",
                table: "PersonalDataSheets",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpouseSuffix",
                table: "PersonalDataSheets",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpouseSurname",
                table: "PersonalDataSheets",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SssNumber",
                table: "PersonalDataSheets",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinNumber",
                table: "PersonalDataSheets",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Identifications",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssuanceDatePlace",
                table: "Identifications",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Suffix",
                table: "Employees",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Employees",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Eligibities",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Eligibities",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Addresses",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Block",
                table: "Addresses",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Addresses",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Addresses",
                maxLength: 15,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ZipCode",
                table: "Addresses",
                maxLength: 10,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Children",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PdsId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 30, nullable: true),
                    Lastname = table.Column<string>(maxLength: 50, nullable: true),
                    Firstname = table.Column<string>(maxLength: 50, nullable: true),
                    Middle = table.Column<string>(maxLength: 50, nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    Suffix = table.Column<string>(maxLength: 10, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Children", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Children_PersonalDataSheets_PdsId",
                        column: x => x.PdsId,
                        principalTable: "PersonalDataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EducationalBackgrounds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PdsId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 30, nullable: true),
                    OrderNumber = table.Column<string>(maxLength: 5, nullable: true),
                    Level = table.Column<string>(maxLength: 30, nullable: true),
                    School = table.Column<string>(maxLength: 150, nullable: true),
                    Course = table.Column<string>(maxLength: 150, nullable: true),
                    AttendanceFrom = table.Column<DateTime>(nullable: true),
                    AttendanceTo = table.Column<DateTime>(nullable: true),
                    HighestLevel = table.Column<string>(maxLength: 15, nullable: true),
                    YearGraduated = table.Column<string>(maxLength: 10, nullable: true),
                    Honors = table.Column<string>(maxLength: 30, nullable: true),
                    showInPds = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalBackgrounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationalBackgrounds_PersonalDataSheets_PdsId",
                        column: x => x.PdsId,
                        principalTable: "PersonalDataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PdsId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 30, nullable: true),
                    Organization = table.Column<string>(maxLength: 100, nullable: true),
                    CertNumber = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Memberships_PersonalDataSheets_PdsId",
                        column: x => x.PdsId,
                        principalTable: "PersonalDataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recognitions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PdsId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 30, nullable: true),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Year = table.Column<string>(maxLength: 10, nullable: true),
                    CertNumber = table.Column<string>(maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recognitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recognitions_PersonalDataSheets_PdsId",
                        column: x => x.PdsId,
                        principalTable: "PersonalDataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "References",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PdsId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 30, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Address = table.Column<string>(maxLength: 100, nullable: true),
                    Mobile = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_References", x => x.Id);
                    table.ForeignKey(
                        name: "FK_References_PersonalDataSheets_PdsId",
                        column: x => x.PdsId,
                        principalTable: "PersonalDataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PdsId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 30, nullable: true),
                    Description = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skills_PersonalDataSheets_PdsId",
                        column: x => x.PdsId,
                        principalTable: "PersonalDataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainingsAttented",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PdsId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 30, nullable: true),
                    Title = table.Column<string>(nullable: true),
                    PeriodFrom = table.Column<DateTime>(nullable: true),
                    PeriodTo = table.Column<DateTime>(nullable: true),
                    Hours = table.Column<float>(nullable: false),
                    LndType = table.Column<string>(maxLength: 30, nullable: true),
                    Sponsor = table.Column<string>(maxLength: 100, nullable: true),
                    TrainingCode = table.Column<string>(maxLength: 15, nullable: true),
                    CertNumber = table.Column<string>(maxLength: 15, nullable: true),
                    Verified = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingsAttented", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingsAttented_PersonalDataSheets_PdsId",
                        column: x => x.PdsId,
                        principalTable: "PersonalDataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoluntaryWorks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PdsId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 30, nullable: true),
                    Address = table.Column<string>(maxLength: 150, nullable: true),
                    PeriodFrom = table.Column<DateTime>(nullable: true),
                    PeriodTo = table.Column<DateTime>(nullable: true),
                    Hours = table.Column<float>(nullable: false),
                    Position = table.Column<string>(maxLength: 150, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoluntaryWorks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoluntaryWorks_PersonalDataSheets_PdsId",
                        column: x => x.PdsId,
                        principalTable: "PersonalDataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkExperiences",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PdsId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 30, nullable: true),
                    PeriodFrom = table.Column<DateTime>(nullable: true),
                    PeriodTo = table.Column<DateTime>(nullable: true),
                    Position = table.Column<string>(maxLength: 100, nullable: true),
                    Company = table.Column<string>(maxLength: 150, nullable: true),
                    MonthlySalary = table.Column<float>(nullable: false),
                    SalaryGrade = table.Column<string>(maxLength: 10, nullable: true),
                    AppointmentStatus = table.Column<string>(maxLength: 30, nullable: true),
                    GovernmentService = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkExperiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkExperiences_PersonalDataSheets_PdsId",
                        column: x => x.PdsId,
                        principalTable: "PersonalDataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalDataSheets_EmployeeId",
                table: "PersonalDataSheets",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Children_PdsId",
                table: "Children",
                column: "PdsId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalBackgrounds_PdsId",
                table: "EducationalBackgrounds",
                column: "PdsId");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_PdsId",
                table: "Memberships",
                column: "PdsId");

            migrationBuilder.CreateIndex(
                name: "IX_Recognitions_PdsId",
                table: "Recognitions",
                column: "PdsId");

            migrationBuilder.CreateIndex(
                name: "IX_References_PdsId",
                table: "References",
                column: "PdsId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_PdsId",
                table: "Skills",
                column: "PdsId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingsAttented_PdsId",
                table: "TrainingsAttented",
                column: "PdsId");

            migrationBuilder.CreateIndex(
                name: "IX_VoluntaryWorks_PdsId",
                table: "VoluntaryWorks",
                column: "PdsId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkExperiences_PdsId",
                table: "WorkExperiences",
                column: "PdsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalDataSheets_Employees_EmployeeId",
                table: "PersonalDataSheets",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalDataSheets_Employees_EmployeeId",
                table: "PersonalDataSheets");

            migrationBuilder.DropTable(
                name: "Children");

            migrationBuilder.DropTable(
                name: "EducationalBackgrounds");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropTable(
                name: "Recognitions");

            migrationBuilder.DropTable(
                name: "References");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "TrainingsAttented");

            migrationBuilder.DropTable(
                name: "VoluntaryWorks");

            migrationBuilder.DropTable(
                name: "WorkExperiences");

            migrationBuilder.DropIndex(
                name: "IX_PersonalDataSheets_EmployeeId",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "AgencyNumber",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "BirthPlace",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "Citizenship",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "DualCitizenCountry",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "DualCitizenType",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "FatherFirstname",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "FatherMiddle",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "FatherSuffix",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "FatherSurname",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "GsisNumber",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "HdmfNumber",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "MotherFirstname",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "MotherMaidenName",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "MotherMiddle",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "MotherSuffix",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "MotherSurname",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "OtherCivilStatus",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "PhilHealthNumber",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "SpouseEmployer",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "SpouseEmployerAddress",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "SpouseEmployerTelNumber",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "SpouseFirstname",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "SpouseMiddle",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "SpouseOccupation",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "SpouseSuffix",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "SpouseSurname",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "SssNumber",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "TinNumber",
                table: "PersonalDataSheets");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Identifications");

            migrationBuilder.DropColumn(
                name: "IssuanceDatePlace",
                table: "Identifications");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Eligibities");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Eligibities");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ZipCode",
                table: "Addresses");

            migrationBuilder.RenameColumn(
                name: "IdType",
                table: "Identifications",
                newName: "IDType");

            migrationBuilder.AddColumn<string>(
                name: "CurrentHolder",
                table: "PersonalDataSheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NextUser",
                table: "PersonalDataSheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousHolder",
                table: "PersonalDataSheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PersonalDataSheets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Suffix",
                table: "Employees",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Addresses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Block",
                table: "Addresses",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30,
                oldNullable: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YamangTao.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BranchCampuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Campus = table.Column<string>(maxLength: 100, nullable: true),
                    Address = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchCampuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobPositions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    SalaryGrade = table.Column<string>(maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KpiTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KpiTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonalDataSheets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(maxLength: 30, nullable: true),
                    CivilStatus = table.Column<string>(maxLength: 30, nullable: true),
                    Height = table.Column<string>(maxLength: 10, nullable: true),
                    Weight = table.Column<string>(maxLength: 10, nullable: true),
                    BloodType = table.Column<string>(maxLength: 5, nullable: true),
                    DateAccomplished = table.Column<DateTime>(nullable: true),
                    ConsanguinityThird = table.Column<bool>(nullable: false),
                    ConsanguinityFouth = table.Column<bool>(nullable: false),
                    ConsanguinityFouthDetails = table.Column<string>(maxLength: 50, nullable: true),
                    AdministrativeOffense = table.Column<bool>(nullable: false),
                    AdministrativeOffenseDetails = table.Column<string>(maxLength: 50, nullable: true),
                    CriminalCharge = table.Column<bool>(nullable: false),
                    CriminalChargeDateFiled = table.Column<DateTime>(nullable: true),
                    CriminalChargeStatus = table.Column<string>(maxLength: 50, nullable: true),
                    Convicted = table.Column<bool>(nullable: false),
                    ConvictedDetails = table.Column<string>(maxLength: 50, nullable: true),
                    SeparatedFromService = table.Column<bool>(nullable: false),
                    SeparatedFromServiceDetails = table.Column<string>(maxLength: 50, nullable: true),
                    ElectionCandidate = table.Column<bool>(nullable: false),
                    ElectionCandidateDetails = table.Column<string>(maxLength: 50, nullable: true),
                    ResignedForElection = table.Column<bool>(nullable: false),
                    ResignedForElectionDetails = table.Column<string>(maxLength: 50, nullable: true),
                    Immigrant = table.Column<bool>(nullable: false),
                    ImmigrantDetails = table.Column<string>(maxLength: 50, nullable: true),
                    IpMember = table.Column<bool>(nullable: false),
                    IpMemberDetails = table.Column<string>(maxLength: 50, nullable: true),
                    PwdMember = table.Column<bool>(nullable: false),
                    PwdMemberDetails = table.Column<string>(maxLength: 50, nullable: true),
                    SoloParent = table.Column<bool>(nullable: false),
                    SoloParentId = table.Column<string>(maxLength: 30, nullable: true),
                    GovIdType = table.Column<string>(maxLength: 30, nullable: true),
                    GovIdNumber = table.Column<string>(nullable: true),
                    GovIdDatePlaceIssued = table.Column<string>(maxLength: 75, nullable: true),
                    DateSubmitted = table.Column<DateTime>(nullable: true),
                    Verified = table.Column<bool>(nullable: false),
                    DateReceived = table.Column<DateTime>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    DateLastModified = table.Column<DateTime>(nullable: true),
                    DateLastPrinted = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalDataSheets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PdsId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 30, nullable: true),
                    Description = table.Column<string>(maxLength: 30, nullable: true),
                    Block = table.Column<string>(maxLength: 10, nullable: true),
                    Street = table.Column<string>(maxLength: 100, nullable: true),
                    Purok = table.Column<string>(maxLength: 30, nullable: true),
                    Barangay = table.Column<string>(maxLength: 30, nullable: true),
                    Municipality = table.Column<string>(maxLength: 30, nullable: true),
                    Province = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_PersonalDataSheets_PdsId",
                        column: x => x.PdsId,
                        principalTable: "PersonalDataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Eligibities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PdsId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 30, nullable: true),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Rating = table.Column<string>(maxLength: 10, nullable: true),
                    ExamDate = table.Column<DateTime>(nullable: true),
                    ExamPlace = table.Column<string>(maxLength: 100, nullable: true),
                    LicenseNumber = table.Column<string>(maxLength: 30, nullable: true),
                    ValidityDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eligibities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Eligibities_PersonalDataSheets_PdsId",
                        column: x => x.PdsId,
                        principalTable: "PersonalDataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Identifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PdsId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<string>(maxLength: 30, nullable: true),
                    IDType = table.Column<string>(maxLength: 30, nullable: true),
                    Control = table.Column<string>(maxLength: 10, nullable: true),
                    DateIssued = table.Column<DateTime>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Identifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Identifications_PersonalDataSheets_PdsId",
                        column: x => x.PdsId,
                        principalTable: "PersonalDataSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(nullable: true),
                    Desciption = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    IsMain = table.Column<bool>(nullable: false),
                    PublicId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 30, nullable: false),
                    EmployeeGroup = table.Column<string>(maxLength: 30, nullable: true),
                    CurrentStatus = table.Column<string>(maxLength: 30, nullable: true),
                    Lastname = table.Column<string>(maxLength: 50, nullable: true),
                    Firstname = table.Column<string>(maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(maxLength: 50, nullable: true),
                    MI = table.Column<string>(maxLength: 3, nullable: true),
                    Suffix = table.Column<string>(maxLength: 3, nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    Sex = table.Column<string>(maxLength: 15, nullable: true),
                    Telephone = table.Column<string>(maxLength: 50, nullable: true),
                    MobileNumber = table.Column<string>(maxLength: 50, nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    InActive = table.Column<bool>(nullable: false),
                    DateHired = table.Column<DateTime>(nullable: true),
                    Resigned = table.Column<bool>(nullable: false),
                    DateResigned = table.Column<DateTime>(nullable: true),
                    Terminated = table.Column<bool>(nullable: false),
                    DateTerminated = table.Column<DateTime>(nullable: true),
                    Retired = table.Column<bool>(nullable: false),
                    DateRetired = table.Column<DateTime>(nullable: true),
                    BrachCampusId = table.Column<int>(nullable: true),
                    CurrentCampusId = table.Column<int>(nullable: true),
                    OrgUnitId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_BranchCampuses_CurrentCampusId",
                        column: x => x.CurrentCampusId,
                        principalTable: "BranchCampuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    KnownAs = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    LastActive = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrgUnits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<int>(nullable: false),
                    UnitName = table.Column<string>(maxLength: 200, nullable: true),
                    Code = table.Column<string>(maxLength: 10, nullable: true),
                    UnitType = table.Column<string>(maxLength: 30, nullable: true),
                    NameOfHead = table.Column<string>(maxLength: 100, nullable: true),
                    CurrentHeadId = table.Column<string>(maxLength: 30, nullable: true),
                    Location = table.Column<string>(maxLength: 150, nullable: true),
                    ParentUnitId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrgUnits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrgUnits_Employees_CurrentHeadId",
                        column: x => x.CurrentHeadId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrgUnits_OrgUnits_ParentUnitId",
                        column: x => x.ParentUnitId,
                        principalTable: "OrgUnits",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ipcrs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsTemplate = table.Column<bool>(nullable: false),
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

            migrationBuilder.CreateTable(
                name: "KPIs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(maxLength: 20, nullable: true),
                    OrderNumber = table.Column<string>(maxLength: 10, nullable: true),
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
                    ParentKpiId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KPIs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KPIs_Ipcrs_IpcrId",
                        column: x => x.IpcrId,
                        principalTable: "Ipcrs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Id = table.Column<long>(nullable: false),
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
                name: "IX_Addresses_PdsId",
                table: "Addresses",
                column: "PdsId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmployeeId",
                table: "AspNetUsers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Eligibities_PdsId",
                table: "Eligibities",
                column: "PdsId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CurrentCampusId",
                table: "Employees",
                column: "CurrentCampusId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_OrgUnitId",
                table: "Employees",
                column: "OrgUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Identifications_PdsId",
                table: "Identifications",
                column: "PdsId");

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
                name: "IX_KPIs_IpcrId",
                table: "KPIs",
                column: "IpcrId");

            migrationBuilder.CreateIndex(
                name: "IX_KPIs_KpiTypeId",
                table: "KPIs",
                column: "KpiTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_KPIs_ParentKpiId",
                table: "KPIs",
                column: "ParentKpiId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgUnits_CurrentHeadId",
                table: "OrgUnits",
                column: "CurrentHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_OrgUnits_ParentUnitId",
                table: "OrgUnits",
                column: "ParentUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Photo_UserId",
                table: "Photo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingMatrix_KpiId",
                table: "RatingMatrix",
                column: "KpiId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Photo_AspNetUsers_UserId",
                table: "Photo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_OrgUnits_OrgUnitId",
                table: "Employees",
                column: "OrgUnitId",
                principalTable: "OrgUnits",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrgUnits_Employees_CurrentHeadId",
                table: "OrgUnits");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Eligibities");

            migrationBuilder.DropTable(
                name: "Identifications");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "PersonalDataSheets");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "RatingMatrix");

            migrationBuilder.DropTable(
                name: "KPIs");

            migrationBuilder.DropTable(
                name: "Ipcrs");

            migrationBuilder.DropTable(
                name: "KpiTypes");

            migrationBuilder.DropTable(
                name: "JobPositions");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "BranchCampuses");

            migrationBuilder.DropTable(
                name: "OrgUnits");
        }
    }
}

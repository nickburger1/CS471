using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A_FGMS.DataLayer.Migrations
{
    public partial class DeploymentSeeders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressLine1 = table.Column<string>(type: "varchar(45)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "varchar(45)", nullable: true),
                    City = table.Column<string>(type: "varchar(45)", nullable: false),
                    State = table.Column<string>(type: "varchar(45)", nullable: false),
                    Zipcode = table.Column<string>(type: "varchar(45)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Tuid);
                });

            migrationBuilder.CreateTable(
                name: "ConditionItems",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Acronym = table.Column<string>(type: "varchar(45)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionItems", x => x.Tuid);
                });

            migrationBuilder.CreateTable(
                name: "DonationTypeItems",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(45)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationTypeItems", x => x.Tuid);
                });

            migrationBuilder.CreateTable(
                name: "EthnicityTypeItems",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(45)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EthnicityTypeItems", x => x.Tuid);
                });

            migrationBuilder.CreateTable(
                name: "ExpenseTypeItems",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(45)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseTypeItems", x => x.Tuid);
                });

            migrationBuilder.CreateTable(
                name: "GenderTypeItems",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(45)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderTypeItems", x => x.Tuid);
                });

            migrationBuilder.CreateTable(
                name: "GrantStipends",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartValue = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrantStipends", x => x.Tuid);
                });

            migrationBuilder.CreateTable(
                name: "IdentifiesAsTypeItems",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(45)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentifiesAsTypeItems", x => x.Tuid);
                });

            migrationBuilder.CreateTable(
                name: "InactiveStatusTypeItems",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(45)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InactiveStatusTypeItems", x => x.Tuid);
                });

            migrationBuilder.CreateTable(
                name: "MealTransportRates",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealRate = table.Column<double>(type: "float", nullable: false),
                    MileageRate = table.Column<double>(type: "float", nullable: false),
                    BusMileageRate = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTransportRates", x => x.Tuid);
                });

            migrationBuilder.CreateTable(
                name: "PTOStipendRates",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PTORate = table.Column<double>(type: "float", nullable: false),
                    StipendRate = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PTOStipendRates", x => x.Tuid);
                });

            migrationBuilder.CreateTable(
                name: "RacialGroupTypeItems",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(45)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RacialGroupTypeItems", x => x.Tuid);
                });

            migrationBuilder.CreateTable(
                name: "ReportPresets",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Preset = table.Column<string>(type: "varchar(max)", nullable: false),
                    SortBy = table.Column<string>(type: "varchar(100)", nullable: false),
                    Current = table.Column<bool>(type: "bit", nullable: false),
                    Former = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Inactive = table.Column<bool>(type: "bit", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportPresets", x => x.Tuid);
                });

            migrationBuilder.CreateTable(
                name: "SchoolCostShares",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(45)", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolCostShares", x => x.Tuid);
                });

            migrationBuilder.CreateTable(
                name: "StudentNeedItems",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Acronym = table.Column<string>(type: "varchar(45)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentNeedItems", x => x.Tuid);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identifier = table.Column<string>(type: "varchar(45)", nullable: true),
                    IsAge5To12 = table.Column<bool>(type: "bit", nullable: true),
                    IsAgeBirthTo5 = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Tuid);
                });

            migrationBuilder.CreateTable(
                name: "TempInfoTypeItems",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(45)", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", nullable: true),
                    TempInfoType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempInfoTypeItems", x => x.Tuid);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", nullable: false),
                    HashedPassword = table.Column<string>(type: "varchar(256)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsReadOnly = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Tuid);
                });

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressTuid = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Principal = table.Column<string>(type: "varchar(100)", nullable: false),
                    Secretary = table.Column<string>(type: "varchar(100)", nullable: false),
                    ContactNumber = table.Column<string>(type: "varchar(20)", nullable: false),
                    StartTime = table.Column<string>(type: "varchar(12)", nullable: false),
                    EndTime = table.Column<string>(type: "varchar(12)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Days = table.Column<string>(type: "varchar(5)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Tuid);
                    table.ForeignKey(
                        name: "FK_Schools_Addresses_AddressTuid",
                        column: x => x.AddressTuid,
                        principalTable: "Addresses",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InKindDonationTypeItems",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(45)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    DonationTypeItemTuid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InKindDonationTypeItems", x => x.Tuid);
                    table.ForeignKey(
                        name: "FK_InKindDonationTypeItems_DonationTypeItems_DonationTypeItemTuid",
                        column: x => x.DonationTypeItemTuid,
                        principalTable: "DonationTypeItems",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InKindExpenseTypeItems",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(45)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<double>(type: "float", nullable: false),
                    ExpenseTypeItemTuid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InKindExpenseTypeItems", x => x.Tuid);
                    table.ForeignKey(
                        name: "FK_InKindExpenseTypeItems_ExpenseTypeItems_ExpenseTypeItemTuid",
                        column: x => x.ExpenseTypeItemTuid,
                        principalTable: "ExpenseTypeItems",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Volunteers",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressTuid = table.Column<int>(type: "int", nullable: true),
                    FirstName = table.Column<string>(type: "varchar(45)", nullable: false),
                    LastName = table.Column<string>(type: "varchar(45)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(45)", nullable: false),
                    AltPhone = table.Column<string>(type: "varchar(45)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SeparatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    GenderTuid = table.Column<int>(type: "int", nullable: false),
                    EthnicityTuid = table.Column<int>(type: "int", nullable: false),
                    RacialGroupTuid = table.Column<int>(type: "int", nullable: false),
                    IdentifiesAsTuid = table.Column<int>(type: "int", nullable: false),
                    IsFamilyOfMilitary = table.Column<bool>(type: "bit", nullable: false),
                    IsVeteran = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<string>(type: "varchar(45)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteers", x => x.Tuid);
                    table.ForeignKey(
                        name: "FK_Volunteers_Addresses_AddressTuid",
                        column: x => x.AddressTuid,
                        principalTable: "Addresses",
                        principalColumn: "Tuid");
                    table.ForeignKey(
                        name: "FK_Volunteers_EthnicityTypeItems_EthnicityTuid",
                        column: x => x.EthnicityTuid,
                        principalTable: "EthnicityTypeItems",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Volunteers_GenderTypeItems_GenderTuid",
                        column: x => x.GenderTuid,
                        principalTable: "GenderTypeItems",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Volunteers_IdentifiesAsTypeItems_IdentifiesAsTuid",
                        column: x => x.IdentifiesAsTuid,
                        principalTable: "IdentifiesAsTypeItems",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Volunteers_RacialGroupTypeItems_RacialGroupTuid",
                        column: x => x.RacialGroupTuid,
                        principalTable: "RacialGroupTypeItems",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentConditions",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentTuid = table.Column<int>(type: "int", nullable: false),
                    ConditionItemTuid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentConditions", x => x.Tuid);
                    table.ForeignKey(
                        name: "FK_StudentConditions_ConditionItems_ConditionItemTuid",
                        column: x => x.ConditionItemTuid,
                        principalTable: "ConditionItems",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentConditions_Students_StudentTuid",
                        column: x => x.StudentTuid,
                        principalTable: "Students",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentNeeds",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentTuid = table.Column<int>(type: "int", nullable: false),
                    StudentNeedItemTuid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentNeeds", x => x.Tuid);
                    table.ForeignKey(
                        name: "FK_StudentNeeds_StudentNeedItems_StudentNeedItemTuid",
                        column: x => x.StudentNeedItemTuid,
                        principalTable: "StudentNeedItems",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentNeeds_Students_StudentTuid",
                        column: x => x.StudentTuid,
                        principalTable: "Students",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SchoolTuid = table.Column<int>(type: "int", nullable: false),
                    ClassroomNumber = table.Column<string>(type: "varchar(45)", nullable: true),
                    ClassroomSize = table.Column<int>(type: "int", nullable: true),
                    GradeLevel = table.Column<string>(type: "varchar(45)", nullable: true),
                    TeacherName = table.Column<string>(type: "varchar(45)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.Tuid);
                    table.ForeignKey(
                        name: "FK_Classrooms_Schools_SchoolTuid",
                        column: x => x.SchoolTuid,
                        principalTable: "Schools",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityLogs",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteerTuid = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Initial = table.Column<string>(type: "varchar(10)", nullable: false),
                    Incident = table.Column<string>(type: "varchar(150)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLogs", x => x.Tuid);
                    table.ForeignKey(
                        name: "FK_ActivityLogs_Volunteers_VolunteerTuid",
                        column: x => x.VolunteerTuid,
                        principalTable: "Volunteers",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnnualChecks",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteerTuid = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    PhotoReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmergencyBeneficiaryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HippaReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhysicalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CarInsuranceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CovidSouDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualChecks", x => x.Tuid);
                    table.ForeignKey(
                        name: "FK_AnnualChecks_Volunteers_VolunteerTuid",
                        column: x => x.VolunteerTuid,
                        principalTable: "Volunteers",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InKindExpenses",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteerTuid = table.Column<int>(type: "int", nullable: false),
                    ExpenseTypeTuid = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(12,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InKindExpenses", x => x.Tuid);
                    table.ForeignKey(
                        name: "FK_InKindExpenses_ExpenseTypeItems_ExpenseTypeTuid",
                        column: x => x.ExpenseTypeTuid,
                        principalTable: "ExpenseTypeItems",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InKindExpenses_Volunteers_VolunteerTuid",
                        column: x => x.VolunteerTuid,
                        principalTable: "Volunteers",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealMileages",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteerTuid = table.Column<int>(type: "int", nullable: false),
                    MealCount = table.Column<int>(type: "int", nullable: false),
                    BusRideCount = table.Column<int>(type: "int", nullable: false),
                    Mileage = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealMileages", x => x.Tuid);
                    table.ForeignKey(
                        name: "FK_MealMileages_Volunteers_VolunteerTuid",
                        column: x => x.VolunteerTuid,
                        principalTable: "Volunteers",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OneTimeChecks",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteerTuid = table.Column<int>(type: "int", nullable: false),
                    HasFilePhoto = table.Column<bool>(type: "bit", nullable: false),
                    HasServiceDescription = table.Column<bool>(type: "bit", nullable: false),
                    HasTrainingSheet = table.Column<bool>(type: "bit", nullable: false),
                    ConfidenceSouDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ServiceStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HasNschc = table.Column<bool>(type: "bit", nullable: false),
                    HasBackgroundCheck = table.Column<bool>(type: "bit", nullable: false),
                    HasIdCopy = table.Column<bool>(type: "bit", nullable: false),
                    NsopwDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IChatDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TrueScreenDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AliasFingerprintDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FieldPrintDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DhsDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TbShotDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneTimeChecks", x => x.Tuid);
                    table.ForeignKey(
                        name: "FK_OneTimeChecks_Volunteers_VolunteerTuid",
                        column: x => x.VolunteerTuid,
                        principalTable: "Volunteers",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PTOStipends",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteerTuid = table.Column<int>(type: "int", nullable: false),
                    RegularHours = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    PtoStart = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    PtoEnd = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    PtoUsed = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    PtoEarned = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    StipendPaid = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    YearToDateHour = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    IsPTOEligible = table.Column<bool>(type: "bit", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PTOStipends", x => x.Tuid);
                    table.ForeignKey(
                        name: "FK_PTOStipends_Volunteers_VolunteerTuid",
                        column: x => x.VolunteerTuid,
                        principalTable: "Volunteers",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReasonsSeparated",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteerTuid = table.Column<int>(type: "int", nullable: false),
                    InactiveStatusTypeItemTuid = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReasonsSeparated", x => x.Tuid);
                    table.ForeignKey(
                        name: "FK_ReasonsSeparated_InactiveStatusTypeItems_InactiveStatusTypeItemTuid",
                        column: x => x.InactiveStatusTypeItemTuid,
                        principalTable: "InactiveStatusTypeItems",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReasonsSeparated_Volunteers_VolunteerTuid",
                        column: x => x.VolunteerTuid,
                        principalTable: "Volunteers",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TempInfoEntries",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteerTuid = table.Column<int>(type: "int", nullable: false),
                    TempInfoTypeItemTuid = table.Column<int>(type: "int", nullable: false),
                    value = table.Column<string>(type: "VARCHAR(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TempInfoEntries", x => x.Tuid);
                    table.ForeignKey(
                        name: "FK_TempInfoEntries_TempInfoTypeItems_TempInfoTypeItemTuid",
                        column: x => x.TempInfoTypeItemTuid,
                        principalTable: "TempInfoTypeItems",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TempInfoEntries_Volunteers_VolunteerTuid",
                        column: x => x.VolunteerTuid,
                        principalTable: "Volunteers",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteerTuid = table.Column<int>(type: "int", nullable: false),
                    ClassroomTuid = table.Column<int>(type: "int", nullable: false),
                    Schedule = table.Column<string>(type: "varchar(500)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.Tuid);
                    table.ForeignKey(
                        name: "FK_Assignments_Classrooms_ClassroomTuid",
                        column: x => x.ClassroomTuid,
                        principalTable: "Classrooms",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assignments_Volunteers_VolunteerTuid",
                        column: x => x.VolunteerTuid,
                        principalTable: "Volunteers",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignmentStudents",
                columns: table => new
                {
                    Tuid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignmentTuid = table.Column<int>(type: "int", nullable: false),
                    StudentTuid = table.Column<int>(type: "int", nullable: false),
                    DesiredOutcome = table.Column<string>(type: "varchar(45)", nullable: false),
                    Date = table.Column<string>(type: "varchar(45)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentStudents", x => x.Tuid);
                    table.ForeignKey(
                        name: "FK_AssignmentStudents_Assignments_AssignmentTuid",
                        column: x => x.AssignmentTuid,
                        principalTable: "Assignments",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignmentStudents_Students_StudentTuid",
                        column: x => x.StudentTuid,
                        principalTable: "Students",
                        principalColumn: "Tuid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ConditionItems",
                columns: new[] { "Tuid", "Acronym", "Description" },
                values: new object[,]
                {
                    { 1, "AD", "Attention Difficulties" },
                    { 2, "AN", "Abused or Neglected" },
                    { 3, "CI", "Child of Incarcerated Parent" },
                    { 4, "CH", "Homeless or Recently Displaced" },
                    { 5, "DD", "Developmental Disibilities" },
                    { 6, "ES", "Emotional / Social Difficulties" },
                    { 7, "HS", "Behavior / Social Difficulties" },
                    { 8, "LB", "Language / Literacy Barriers" },
                    { 9, "LD", "Learning Disabilities" },
                    { 10, "PD", "Physical Disabilities" },
                    { 11, "SP", "Speech Impaired" }
                });

            migrationBuilder.InsertData(
                table: "EthnicityTypeItems",
                columns: new[] { "Tuid", "Name" },
                values: new object[,]
                {
                    { 1, "Hispanic" },
                    { 2, "Non-Hispanic" }
                });

            migrationBuilder.InsertData(
                table: "GenderTypeItems",
                columns: new[] { "Tuid", "Name" },
                values: new object[,]
                {
                    { 1, "Female" },
                    { 2, "Male" },
                    { 3, "Transgender" },
                    { 4, "Non-binary/non-conforming" },
                    { 5, "Prefer not to respond" }
                });

            migrationBuilder.InsertData(
                table: "IdentifiesAsTypeItems",
                columns: new[] { "Tuid", "Name" },
                values: new object[,]
                {
                    { 1, "Woman" },
                    { 2, "Man" },
                    { 3, "Non-binary" },
                    { 4, "Undeclared" }
                });

            migrationBuilder.InsertData(
                table: "InactiveStatusTypeItems",
                columns: new[] { "Tuid", "Name" },
                values: new object[,]
                {
                    { 1, "Moved" },
                    { 2, "Terminated" },
                    { 3, "Resigned/Health" },
                    { 4, "Resigned/New Work or Interests" },
                    { 5, "Resigned/Other" },
                    { 6, "Deceased" },
                    { 7, "Transferred" },
                    { 8, "Retired" }
                });

            migrationBuilder.InsertData(
                table: "RacialGroupTypeItems",
                columns: new[] { "Tuid", "Name" },
                values: new object[,]
                {
                    { 1, "Black" },
                    { 2, "White" },
                    { 3, "Asian" },
                    { 4, "Other" }
                });

            migrationBuilder.InsertData(
                table: "StudentNeedItems",
                columns: new[] { "Tuid", "Acronym", "Description" },
                values: new object[,]
                {
                    { 1, "A", "Nurturing / Comfort" },
                    { 2, "B", "Social Skilles" },
                    { 3, "C", "Communication Skills" },
                    { 4, "D", "Reading" },
                    { 5, "E", "Help with Letter Identification" },
                    { 6, "F", "Positive Reinforcement / Redirection" },
                    { 7, "G", "Conversion Skills" },
                    { 8, "H", "Help with Numeracy Skills" }
                });

            migrationBuilder.InsertData(
                table: "StudentNeedItems",
                columns: new[] { "Tuid", "Acronym", "Description" },
                values: new object[] { 9, "I", "Assist with Cognitive Activities" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Tuid", "Email", "HashedPassword", "IsActive", "IsAdmin", "IsDeleted", "IsReadOnly", "Name", "PhoneNumber" },
                values: new object[] { 1, "svsu.developer@svsu.edu", "AAAAAAIAAYagAAAAEOzgQmjJNUK60RAtv8EeQmEZ3q++aBgIhrVLdpHk6ywewdwf5U5eFiVPnK242weLOw==", true, true, false, true, "SVSU Developer", "(989) 964-4000" });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLogs_VolunteerTuid",
                table: "ActivityLogs",
                column: "VolunteerTuid");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualChecks_VolunteerTuid",
                table: "AnnualChecks",
                column: "VolunteerTuid");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ClassroomTuid",
                table: "Assignments",
                column: "ClassroomTuid");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_VolunteerTuid",
                table: "Assignments",
                column: "VolunteerTuid");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentStudents_AssignmentTuid",
                table: "AssignmentStudents",
                column: "AssignmentTuid");

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentStudents_StudentTuid",
                table: "AssignmentStudents",
                column: "StudentTuid");

            migrationBuilder.CreateIndex(
                name: "IX_Classrooms_SchoolTuid",
                table: "Classrooms",
                column: "SchoolTuid");

            migrationBuilder.CreateIndex(
                name: "IX_InKindDonationTypeItems_DonationTypeItemTuid",
                table: "InKindDonationTypeItems",
                column: "DonationTypeItemTuid");

            migrationBuilder.CreateIndex(
                name: "IX_InKindExpenses_ExpenseTypeTuid",
                table: "InKindExpenses",
                column: "ExpenseTypeTuid");

            migrationBuilder.CreateIndex(
                name: "IX_InKindExpenses_VolunteerTuid",
                table: "InKindExpenses",
                column: "VolunteerTuid");

            migrationBuilder.CreateIndex(
                name: "IX_InKindExpenseTypeItems_ExpenseTypeItemTuid",
                table: "InKindExpenseTypeItems",
                column: "ExpenseTypeItemTuid");

            migrationBuilder.CreateIndex(
                name: "IX_MealMileages_VolunteerTuid",
                table: "MealMileages",
                column: "VolunteerTuid");

            migrationBuilder.CreateIndex(
                name: "IX_OneTimeChecks_VolunteerTuid",
                table: "OneTimeChecks",
                column: "VolunteerTuid");

            migrationBuilder.CreateIndex(
                name: "IX_PTOStipends_VolunteerTuid",
                table: "PTOStipends",
                column: "VolunteerTuid");

            migrationBuilder.CreateIndex(
                name: "IX_ReasonsSeparated_InactiveStatusTypeItemTuid",
                table: "ReasonsSeparated",
                column: "InactiveStatusTypeItemTuid");

            migrationBuilder.CreateIndex(
                name: "IX_ReasonsSeparated_VolunteerTuid",
                table: "ReasonsSeparated",
                column: "VolunteerTuid");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_AddressTuid",
                table: "Schools",
                column: "AddressTuid");

            migrationBuilder.CreateIndex(
                name: "IX_StudentConditions_ConditionItemTuid",
                table: "StudentConditions",
                column: "ConditionItemTuid");

            migrationBuilder.CreateIndex(
                name: "IX_StudentConditions_StudentTuid",
                table: "StudentConditions",
                column: "StudentTuid");

            migrationBuilder.CreateIndex(
                name: "IX_StudentNeeds_StudentNeedItemTuid",
                table: "StudentNeeds",
                column: "StudentNeedItemTuid");

            migrationBuilder.CreateIndex(
                name: "IX_StudentNeeds_StudentTuid",
                table: "StudentNeeds",
                column: "StudentTuid");

            migrationBuilder.CreateIndex(
                name: "IX_TempInfoEntries_TempInfoTypeItemTuid",
                table: "TempInfoEntries",
                column: "TempInfoTypeItemTuid");

            migrationBuilder.CreateIndex(
                name: "IX_TempInfoEntries_VolunteerTuid",
                table: "TempInfoEntries",
                column: "VolunteerTuid");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_AddressTuid",
                table: "Volunteers",
                column: "AddressTuid");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_EthnicityTuid",
                table: "Volunteers",
                column: "EthnicityTuid");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_GenderTuid",
                table: "Volunteers",
                column: "GenderTuid");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_IdentifiesAsTuid",
                table: "Volunteers",
                column: "IdentifiesAsTuid");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_RacialGroupTuid",
                table: "Volunteers",
                column: "RacialGroupTuid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityLogs");

            migrationBuilder.DropTable(
                name: "AnnualChecks");

            migrationBuilder.DropTable(
                name: "AssignmentStudents");

            migrationBuilder.DropTable(
                name: "GrantStipends");

            migrationBuilder.DropTable(
                name: "InKindDonationTypeItems");

            migrationBuilder.DropTable(
                name: "InKindExpenses");

            migrationBuilder.DropTable(
                name: "InKindExpenseTypeItems");

            migrationBuilder.DropTable(
                name: "MealMileages");

            migrationBuilder.DropTable(
                name: "MealTransportRates");

            migrationBuilder.DropTable(
                name: "OneTimeChecks");

            migrationBuilder.DropTable(
                name: "PTOStipendRates");

            migrationBuilder.DropTable(
                name: "PTOStipends");

            migrationBuilder.DropTable(
                name: "ReasonsSeparated");

            migrationBuilder.DropTable(
                name: "ReportPresets");

            migrationBuilder.DropTable(
                name: "SchoolCostShares");

            migrationBuilder.DropTable(
                name: "StudentConditions");

            migrationBuilder.DropTable(
                name: "StudentNeeds");

            migrationBuilder.DropTable(
                name: "TempInfoEntries");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "DonationTypeItems");

            migrationBuilder.DropTable(
                name: "ExpenseTypeItems");

            migrationBuilder.DropTable(
                name: "InactiveStatusTypeItems");

            migrationBuilder.DropTable(
                name: "ConditionItems");

            migrationBuilder.DropTable(
                name: "StudentNeedItems");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "TempInfoTypeItems");

            migrationBuilder.DropTable(
                name: "Classrooms");

            migrationBuilder.DropTable(
                name: "Volunteers");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropTable(
                name: "EthnicityTypeItems");

            migrationBuilder.DropTable(
                name: "GenderTypeItems");

            migrationBuilder.DropTable(
                name: "IdentifiesAsTypeItems");

            migrationBuilder.DropTable(
                name: "RacialGroupTypeItems");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}

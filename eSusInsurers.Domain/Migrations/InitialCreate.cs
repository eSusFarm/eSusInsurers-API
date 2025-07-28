using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eSusInsurers.Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppEvents",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AppEvent__7944C810F5EE2443", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "AppEvents_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    EventName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__AppEvent__5F38963830D31048", x => x.HistoryRowId);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationMenu",
                columns: table => new
                {
                    ApplicationMenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationMenuName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApplicationMenuLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ApplicationMenuIcon = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    ApplicationMenuIcon2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Applicat__E37CD6C7CFC1C799", x => x.ApplicationMenuId);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationSettings",
                columns: table => new
                {
                    ApplicationSettingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSettings", x => x.ApplicationSettingId);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    CountryCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Countrie__10D1609F96CFAFE2", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Countries_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    CountryName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    CountryCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Countrie__5F389638EAA36072", x => x.HistoryRowId);
                });

            migrationBuilder.CreateTable(
                name: "CropCategories",
                columns: table => new
                {
                    CropCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CropCategoryName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true),
                    SequenceId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CropCate__EA6546C4C67F2FE7", x => x.CropCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "CropCategories_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CropCategoryId = table.Column<int>(type: "int", nullable: false),
                    CropCategoryName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true),
                    SequenceId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CropCate__5F389638F58D33A9", x => x.HistoryRowId);
                });

            migrationBuilder.CreateTable(
                name: "CropInsuranceCategory_Au",
                columns: table => new
                {
                    AuditId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    CategoryName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    ActionType = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ActionTimestamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CropInsu__A17F23987788E9FB", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "EsusFarmPolicy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolicyId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnchainId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PremiumAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RiskId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubscriptionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SumInsuredAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ResponseData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSuccess = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EsusFarmPolicy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FarmerCrops_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    FarmerCropId = table.Column<int>(type: "int", nullable: false),
                    FarmerId = table.Column<int>(type: "int", nullable: true),
                    CropId = table.Column<int>(type: "int", nullable: true),
                    FarmLandSize = table.Column<decimal>(type: "decimal(10,5)", nullable: true),
                    IsPrecultCompleted = table.Column<bool>(type: "bit", nullable: true),
                    IsCultCompleted = table.Column<bool>(type: "bit", nullable: true),
                    IsPlantGrowthCompleted = table.Column<bool>(type: "bit", nullable: true),
                    IsHarvestCompleted = table.Column<bool>(type: "bit", nullable: true),
                    PreCultStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PreCultEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CultStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CultEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PlantGrowthStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PlantGrowthEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    HarvestingStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    HarvestingEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    eSusPoints = table.Column<int>(type: "int", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FarmerAUCr__56CFA48E155F5C1D", x => x.HistoryRowId);
                });

            migrationBuilder.CreateTable(
                name: "Farmers",
                columns: table => new
                {
                    FarmerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MobileNumber = table.Column<decimal>(type: "numeric(15,0)", nullable: true),
                    MSISDN = table.Column<decimal>(type: "numeric(15,0)", nullable: true),
                    Password = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    MobilePIN = table.Column<decimal>(type: "numeric(10,0)", nullable: true),
                    Name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    TnCAccepted = table.Column<bool>(type: "bit", nullable: true),
                    Surname = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    DOB = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IDNumber = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Gender = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    Address = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    City = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Province = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    ProgramName = table.Column<bool>(type: "bit", nullable: true),
                    EnterProgramName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    FarmingCommodity = table.Column<int>(type: "int", nullable: true),
                    FarmSize = table.Column<decimal>(type: "numeric(10,0)", nullable: true),
                    DataConfirmation = table.Column<bool>(type: "bit", nullable: true),
                    MainOrTraditionalLand = table.Column<int>(type: "int", nullable: true),
                    StreetName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ChiefName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    VillageName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    RiverName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    LevelOfEducation = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    DipTank = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    NearestMountain = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    NearestPostOffice = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    ProgramId = table.Column<int>(type: "int", nullable: true),
                    isFarmerDeletedbySuperAdmin = table.Column<bool>(type: "bit", nullable: false),
                    isSuspended = table.Column<bool>(type: "bit", nullable: false),
                    Comments = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(20,10)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(20,10)", nullable: true),
                    Location = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ProfilePicture = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IDFrontView = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IDBackView = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    AdminComments = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    ServiceProvider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Farmers__731B8888BD364271", x => x.FarmerId);
                });

            migrationBuilder.CreateTable(
                name: "Farmers_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FarmerId = table.Column<int>(type: "int", nullable: false),
                    MobileNumber = table.Column<decimal>(type: "numeric(15,0)", nullable: true),
                    MSISDN = table.Column<decimal>(type: "numeric(15,0)", nullable: true),
                    Password = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    MobilePIN = table.Column<decimal>(type: "numeric(10,0)", nullable: true),
                    Name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    TnCAccepted = table.Column<bool>(type: "bit", nullable: true),
                    Surname = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    DOB = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IDNumber = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Gender = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    Address = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    City = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Province = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    ProgramName = table.Column<bool>(type: "bit", nullable: true),
                    EnterProgramName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    FarmingCommodity = table.Column<int>(type: "int", nullable: true),
                    FarmSize = table.Column<decimal>(type: "numeric(10,0)", nullable: true),
                    DataConfirmation = table.Column<bool>(type: "bit", nullable: true),
                    MainOrTraditionalLand = table.Column<int>(type: "int", nullable: true),
                    StreetName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ChiefName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    VillageName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    RiverName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    LevelOfEducation = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    DipTank = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    NearestMountain = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    NearestPostOffice = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    ProgramId = table.Column<int>(type: "int", nullable: true),
                    isFarmerDeletedbySuperAdmin = table.Column<bool>(type: "bit", nullable: false),
                    isSuspended = table.Column<bool>(type: "bit", nullable: false),
                    Comments = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(20,10)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(20,10)", nullable: true),
                    Location = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ProfilePicture = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IDFrontView = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IDBackView = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    AdminComments = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    ServiceProvider = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Farmers___5F38963857558BB3", x => x.HistoryRowId);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    FeatureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Feature = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Features__82230BC9A0EFB064", x => x.FeatureId);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceCompany",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Insuranc__2D971CAC94366EC5", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceCompany_Au",
                columns: table => new
                {
                    AuditId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CompanyName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    ActionType = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ActionTimestamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Insuranc__A17F2398AB117D6F", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "InsurancePolicy_Au",
                columns: table => new
                {
                    AuditId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsurancePolicyId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    PolicyName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    ActionType = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ActionTimestamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CreatedBy = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Insuranc__A17F23986DBA7219", x => x.AuditId);
                });

            migrationBuilder.CreateTable(
                name: "InsuranceRequests",
                columns: table => new
                {
                    InsuranceRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FarmerId = table.Column<int>(type: "int", nullable: false),
                    FarmerCropId = table.Column<int>(type: "int", nullable: false),
                    FarmerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CropName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FarmLocationDistrict = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FarmLocationSubCounty = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FarmLocationParish = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FarmLocationWard = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FarmLocationVillage = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsFarmLocationSameAsHomeLocation = table.Column<bool>(type: "bit", nullable: true),
                    HomeLocationDistrict = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    HomeLocationSubCounty = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    HomeLocationParish = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    HomeLocationWard = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    HomeLocationVillage = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ProgramName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    InsuranceCompanyName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsRequestSubmitted = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Insuranc__6E56996F64692F25", x => x.InsuranceRequestId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentModes",
                columns: table => new
                {
                    PaymentModeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMode = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentM__F9599549EA41D054", x => x.PaymentModeId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentModes_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    PaymentModeId = table.Column<int>(type: "int", nullable: false),
                    PaymentMode = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentM__5F389638FC7BC967", x => x.HistoryRowId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    ReportingToId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__8AFACE1AA64719AD", x => x.RoleId);
                    table.ForeignKey(
                        name: "FK_Roles_Roles_ReportingToId",
                        column: x => x.ReportingToId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    SeasonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeasonName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SeasonYear = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Seasons__C1814E3868BF64A5", x => x.SeasonId);
                });

            migrationBuilder.CreateTable(
                name: "TaxComponents",
                columns: table => new
                {
                    TaxComponentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaxComponent = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Value = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TaxCompo__E333E7C11587A132", x => x.TaxComponentId);
                });

            migrationBuilder.CreateTable(
                name: "TaxComponents_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    TaxComponentId = table.Column<int>(type: "int", nullable: false),
                    TaxComponent = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Value = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TaxCompo__5F3896381C36D00C", x => x.HistoryRowId);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationChildMenu",
                columns: table => new
                {
                    ApplicationChildMenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationMenuId = table.Column<int>(type: "int", nullable: false),
                    ApplicationChildMenuName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApplicationChildMenuLink = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ApplicationChildMenuIcon = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false),
                    ApplicationChildMenuIcon2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Applicat__0BE735C316236FA9", x => x.ApplicationChildMenuId);
                    table.ForeignKey(
                        name: "FK__Applicati__Appli__567ED357",
                        column: x => x.ApplicationMenuId,
                        principalTable: "ApplicationMenu",
                        principalColumn: "ApplicationMenuId");
                });

            migrationBuilder.CreateTable(
                name: "InsuranceProviders",
                columns: table => new
                {
                    InsurerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsurerName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    TaxIdentificationNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ContactPersonName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    ContactPersonMobileNumber = table.Column<decimal>(type: "numeric(15,0)", nullable: false),
                    ContactPersonAlternateContactNumber = table.Column<decimal>(type: "numeric(15,0)", nullable: true),
                    ContactPersonEmailId = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false),
                    ContactPersonAlternateEmailId = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    HeadOfficeAddress = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false),
                    EmailId1 = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false),
                    EmailId2 = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    ContactNumber1 = table.Column<decimal>(type: "numeric(15,0)", nullable: false),
                    ContactNumber2 = table.Column<decimal>(type: "numeric(15,0)", nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IsInsurerVerified = table.Column<bool>(type: "bit", nullable: false),
                    Logo = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(8,6)", nullable: true),
                    Comments = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Address = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Insuranc__7E508CE610302A5F", x => x.InsurerId);
                    table.ForeignKey(
                        name: "FK_Countries",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId");
                });

            migrationBuilder.CreateTable(
                name: "InsuranceProviders_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    InsurerId = table.Column<int>(type: "int", nullable: false),
                    InsurerName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    TaxIdentificationNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ContactPersonName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    ContactPersonMobileNumber = table.Column<decimal>(type: "numeric(15,0)", nullable: false),
                    ContactPersonAlternateContactNumber = table.Column<decimal>(type: "numeric(15,0)", nullable: true),
                    ContactPersonEmailId = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false),
                    ContactPersonAlternateEmailId = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    HeadOfficeAddress = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false),
                    EmailId1 = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false),
                    EmailId2 = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    ContactNumber1 = table.Column<decimal>(type: "numeric(15,0)", nullable: false),
                    ContactNumber2 = table.Column<decimal>(type: "numeric(15,0)", nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IsInsurerVerified = table.Column<bool>(type: "bit", nullable: false),
                    Logo = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(8,6)", nullable: true),
                    Comments = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Address = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Insuranc__5F389638E67798D3", x => x.HistoryRowId);
                    table.ForeignKey(
                        name: "FK_Countries_AU",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId");
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Regions__ACD844A3D37510A7", x => x.RegionId);
                    table.ForeignKey(
                        name: "FK_Countries_Regions",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId");
                });

            migrationBuilder.CreateTable(
                name: "Regions_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    RegionName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Regions___5F3896382C236203", x => x.HistoryRowId);
                    table.ForeignKey(
                        name: "FK_Countries_Regions_AU",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "CountryId");
                });

            migrationBuilder.CreateTable(
                name: "Crops",
                columns: table => new
                {
                    CropId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CropName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CropCategoryId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MinOrderQuantity = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    QuantityUnits = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    SequenceId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Crops__92356115E929DB23", x => x.CropId);
                    table.ForeignKey(
                        name: "FK__Crops__CropCateg__36470DEF",
                        column: x => x.CropCategoryId,
                        principalTable: "CropCategories",
                        principalColumn: "CropCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Crops_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CropId = table.Column<int>(type: "int", nullable: false),
                    CropName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CropCategoryId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    MinOrderQuantity = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    QuantityUnits = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    SequenceId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Crops_AU__5F389638E1FA4A86", x => x.HistoryRowId);
                    table.ForeignKey(
                        name: "FK__Crops_AU__CropCa__3B0BC30C",
                        column: x => x.CropCategoryId,
                        principalTable: "CropCategories",
                        principalColumn: "CropCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Functionalities",
                columns: table => new
                {
                    FunctionalityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    Functionality = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Function__722E3CFEEA16B5C6", x => x.FunctionalityId);
                    table.ForeignKey(
                        name: "FK_Features",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "FeatureId");
                });

            migrationBuilder.CreateTable(
                name: "Functionalities_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    FunctionalityId = table.Column<int>(type: "int", nullable: false),
                    FeatureId = table.Column<int>(type: "int", nullable: false),
                    Functionality = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Function__5F3896385157E66F", x => x.HistoryRowId);
                    table.ForeignKey(
                        name: "FK_Features_AU",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "FeatureId");
                });

            migrationBuilder.CreateTable(
                name: "CropInsuranceCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CropInsu__19093A0B53ECEF8C", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK__CropInsur__Compa__04459E07",
                        column: x => x.CompanyId,
                        principalTable: "InsuranceCompany",
                        principalColumn: "CompanyId");
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    TemplateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    MailSubject = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    MailContent = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    MailTo = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Cc = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Bcc = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EmailTemplates__TemplateId", x => x.TemplateId);
                    table.ForeignKey(
                        name: "FK_EmailTemplates_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                    table.ForeignKey(
                        name: "FK__EmailTemplates__EventId",
                        column: x => x.EventId,
                        principalTable: "AppEvents",
                        principalColumn: "EventId");
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    MailSubject = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    MailContent = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    MailTo = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    Cc = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Bcc = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EmailTemplates__TemplateId_AU", x => x.HistoryRowId);
                    table.ForeignKey(
                        name: "FK_EmailTemplates_RoleId_AU",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                    table.ForeignKey(
                        name: "FK__EmailTemplates__EventId_AU",
                        column: x => x.EventId,
                        principalTable: "AppEvents",
                        principalColumn: "EventId");
                });

            migrationBuilder.CreateTable(
                name: "ApplicationFunctionalities",
                columns: table => new
                {
                    ApplicationFunctionalityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Functionality = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    ApplicationMenuId = table.Column<int>(type: "int", nullable: false),
                    ApplicationChildMenuId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Applicat__BDD9B3C4B3731912", x => x.ApplicationFunctionalityId);
                    table.ForeignKey(
                        name: "FK__Applicati__Appli__5772F790",
                        column: x => x.ApplicationMenuId,
                        principalTable: "ApplicationMenu",
                        principalColumn: "ApplicationMenuId");
                    table.ForeignKey(
                        name: "FK__Applicati__Appli__58671BC9",
                        column: x => x.ApplicationChildMenuId,
                        principalTable: "ApplicationChildMenu",
                        principalColumn: "ApplicationChildMenuId");
                });

            migrationBuilder.CreateTable(
                name: "MenuRolesPrivileges",
                columns: table => new
                {
                    MenuRolesPrivilegeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ApplicationMenuId = table.Column<int>(type: "int", nullable: false),
                    ApplicationChildMenuId = table.Column<int>(type: "int", nullable: true),
                    Read = table.Column<bool>(type: "bit", nullable: false),
                    Create = table.Column<bool>(type: "bit", nullable: false),
                    Update = table.Column<bool>(type: "bit", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MenuRole__FD265CFDD6BDBA7B", x => x.MenuRolesPrivilegeId);
                    table.ForeignKey(
                        name: "FK__MenuRoles__Appli__5E1FF51F",
                        column: x => x.ApplicationMenuId,
                        principalTable: "ApplicationMenu",
                        principalColumn: "ApplicationMenuId");
                    table.ForeignKey(
                        name: "FK__MenuRoles__Appli__5F141958",
                        column: x => x.ApplicationChildMenuId,
                        principalTable: "ApplicationChildMenu",
                        principalColumn: "ApplicationChildMenuId");
                    table.ForeignKey(
                        name: "FK__MenuRoles__RoleI__60083D91",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "InsurancePolicies",
                columns: table => new
                {
                    InsurancePolicyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    PolicyName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    InsuranceProviderId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Insuranc__8D74AD1FF1405620", x => x.InsurancePolicyId);
                    table.ForeignKey(
                        name: "FK_InsuranceProviders_InsurancePolicy",
                        column: x => x.InsuranceProviderId,
                        principalTable: "InsuranceProviders",
                        principalColumn: "InsurerId");
                });

            migrationBuilder.CreateTable(
                name: "InsurancePolicies_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    InsurancePolicyId = table.Column<int>(type: "int", nullable: false),
                    PolicyNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    PolicyName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    InsuranceProviderId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Insuranc__5F3896386A160592", x => x.HistoryRowId);
                    table.ForeignKey(
                        name: "FK_InsuranceProviders_InsurancePolicy_AU",
                        column: x => x.InsuranceProviderId,
                        principalTable: "InsuranceProviders",
                        principalColumn: "InsurerId");
                });

            migrationBuilder.CreateTable(
                name: "InsuranceProviderDocuments",
                columns: table => new
                {
                    InsuranceProviderDocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsurerId = table.Column<int>(type: "int", nullable: false),
                    DocumentName = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    DocumentPath = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Insuranc__AE9929CD8975B86D", x => x.InsuranceProviderDocumentId);
                    table.ForeignKey(
                        name: "FK_InsuranceProviders_InsuranceProviderDocuments",
                        column: x => x.InsurerId,
                        principalTable: "InsuranceProviders",
                        principalColumn: "InsurerId");
                });

            migrationBuilder.CreateTable(
                name: "InsuranceProviderDocuments_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    InsuranceProviderDocumentId = table.Column<int>(type: "int", nullable: false),
                    InsurerId = table.Column<int>(type: "int", nullable: false),
                    DocumentName = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    DocumentPath = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Insuranc__5F389638AF17B6E4", x => x.HistoryRowId);
                    table.ForeignKey(
                        name: "FK_InsuranceProviders_InsuranceProviderDocuments_AU",
                        column: x => x.InsurerId,
                        principalTable: "InsuranceProviders",
                        principalColumn: "InsurerId");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    EmailId = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    ContactNumber = table.Column<decimal>(type: "numeric(15,0)", nullable: false),
                    IsAgent = table.Column<bool>(type: "bit", nullable: false),
                    InsurerId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PasswordSalt = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    PasswordHash = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    IsEnforcePassword = table.Column<bool>(type: "bit", nullable: false),
                    LastLoggedInDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ProfilePicture = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    RefreshToken = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(name: "RefreshTokenExpiryTime ", type: "datetime", nullable: true),
                    Otp = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    OtpExipiryTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ReportingTo = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CC4CE0CE872F", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_InsuranceProviders",
                        column: x => x.InsurerId,
                        principalTable: "InsuranceProviders",
                        principalColumn: "InsurerId");
                    table.ForeignKey(
                        name: "FK_Users_Roles",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                    table.ForeignKey(
                        name: "FK_Users_Users",
                        column: x => x.ReportingTo,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__District__85FDA4C69C6E45B1", x => x.DistrictId);
                    table.ForeignKey(
                        name: "FK_Regions_Districts",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId");
                });

            migrationBuilder.CreateTable(
                name: "Districts_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    DistrictName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__District__5F389638EF323A8C", x => x.HistoryRowId);
                    table.ForeignKey(
                        name: "FK_Regions_Districts_AU",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId");
                });

            migrationBuilder.CreateTable(
                name: "FarmerCrops",
                columns: table => new
                {
                    FarmerCropId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FarmerId = table.Column<int>(type: "int", nullable: true),
                    CropId = table.Column<int>(type: "int", nullable: true),
                    FarmLandSize = table.Column<decimal>(type: "decimal(10,5)", nullable: true),
                    IsPrecultCompleted = table.Column<bool>(type: "bit", nullable: true),
                    IsCultCompleted = table.Column<bool>(type: "bit", nullable: true),
                    IsPlantGrowthCompleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    IsHarvestCompleted = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    PreCultStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PreCultEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CultStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CultEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PlantGrowthStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PlantGrowthEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    HarvestingStartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    HarvestingEndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    eSusPoints = table.Column<int>(type: "int", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FarmerCr__56CFA48E155F5C1D", x => x.FarmerCropId);
                    table.ForeignKey(
                        name: "FK__FarmerCro__CropI__08B54D69",
                        column: x => x.CropId,
                        principalTable: "Crops",
                        principalColumn: "CropId");
                    table.ForeignKey(
                        name: "FK__FarmerCro__Farme__07C12930",
                        column: x => x.FarmerId,
                        principalTable: "Farmers",
                        principalColumn: "FarmerId");
                });

            migrationBuilder.CreateTable(
                name: "SeasonCutOffDates",
                columns: table => new
                {
                    SeasonCutOffDateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeasonId = table.Column<int>(type: "int", nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    CropCategoryId = table.Column<int>(type: "int", nullable: true),
                    CropId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SeasonCu__8CC0AA5F20ECD371", x => x.SeasonCutOffDateId);
                    table.ForeignKey(
                        name: "FK_SeasonCutOffDates_CropCategories_CropCategoryId",
                        column: x => x.CropCategoryId,
                        principalTable: "CropCategories",
                        principalColumn: "CropCategoryId");
                    table.ForeignKey(
                        name: "FK_SeasonCutOffDates_Crops_CropId",
                        column: x => x.CropId,
                        principalTable: "Crops",
                        principalColumn: "CropId");
                    table.ForeignKey(
                        name: "FK_SeasonCutOffDates_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId");
                    table.ForeignKey(
                        name: "FK_SeasonCutOffDates_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "SeasonId");
                });

            migrationBuilder.CreateTable(
                name: "SubFunctionalities",
                columns: table => new
                {
                    SubFunctionalityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubFunctionality = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    FunctionalityId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SubFunct__BFAB7EFF988FD313", x => x.SubFunctionalityId);
                    table.ForeignKey(
                        name: "FK_Functionalities",
                        column: x => x.FunctionalityId,
                        principalTable: "Functionalities",
                        principalColumn: "FunctionalityId");
                });

            migrationBuilder.CreateTable(
                name: "SubFunctionalities_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SubFunctionalityId = table.Column<int>(type: "int", nullable: false),
                    SubFunctionality = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    FunctionalityId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SubFunct__5F3896385A9B63AB", x => x.HistoryRowId);
                    table.ForeignKey(
                        name: "FK_Functionalities_AU",
                        column: x => x.FunctionalityId,
                        principalTable: "Functionalities",
                        principalColumn: "FunctionalityId");
                });

            migrationBuilder.CreateTable(
                name: "InsurancePolicy",
                columns: table => new
                {
                    InsurancePolicyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    PolicyName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedBy = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Insuranc__8D74AD1F32569A1F", x => x.InsurancePolicyId);
                    table.ForeignKey(
                        name: "FK__Insurance__Categ__090A5324",
                        column: x => x.CategoryId,
                        principalTable: "CropInsuranceCategory",
                        principalColumn: "CategoryId");
                    table.ForeignKey(
                        name: "FK__Insurance__Compa__08162EEB",
                        column: x => x.CompanyId,
                        principalTable: "InsuranceCompany",
                        principalColumn: "CompanyId");
                });

            migrationBuilder.CreateTable(
                name: "FunctionalityApprovalProcess",
                columns: table => new
                {
                    FunctionalityApprovalProcessId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationFunctionalityId = table.Column<int>(type: "int", nullable: false),
                    ApprovalProcess = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionalityApprovalProcess", x => x.FunctionalityApprovalProcessId);
                    table.ForeignKey(
                        name: "FK_FunctionalityApprovalProcess_ApplicationFunctionalities_ApplicationFunctionalityId",
                        column: x => x.ApplicationFunctionalityId,
                        principalTable: "ApplicationFunctionalities",
                        principalColumn: "ApplicationFunctionalityId");
                });

            migrationBuilder.CreateTable(
                name: "MenuRolesFunctionalities",
                columns: table => new
                {
                    MenuRolesFunctionalityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ApplicationFunctionalityId = table.Column<int>(type: "int", nullable: false),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MenuRole__074CCD71B3141746", x => x.MenuRolesFunctionalityId);
                    table.ForeignKey(
                        name: "FK__MenuRoles__Appli__5C37ACAD",
                        column: x => x.ApplicationFunctionalityId,
                        principalTable: "ApplicationFunctionalities",
                        principalColumn: "ApplicationFunctionalityId");
                    table.ForeignKey(
                        name: "FK__MenuRoles__RoleI__5D2BD0E6",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "SubCounties",
                columns: table => new
                {
                    SubCountyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCountyName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SubCount__11B0FF6F27F77664", x => x.SubCountyId);
                    table.ForeignKey(
                        name: "FK_Districts_SubCounties",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "DistrictId");
                });

            migrationBuilder.CreateTable(
                name: "SubCounties_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SubCountyId = table.Column<int>(type: "int", nullable: false),
                    SubCountyName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SubCount__5F3896385659FD34", x => x.HistoryRowId);
                    table.ForeignKey(
                        name: "FK_Districts_SubCounties_AU",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "DistrictId");
                });

            migrationBuilder.CreateTable(
                name: "MenuRoleFunctionalityApprovalProcess",
                columns: table => new
                {
                    MenuRoleFunctionalityApprovalProcessId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: true),
                    FunctionalityApprovalProcessId = table.Column<int>(type: "int", nullable: false),
                    Enable = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuRoleFunctionalityApprovalProcess", x => x.MenuRoleFunctionalityApprovalProcessId);
                    table.ForeignKey(
                        name: "FK_MenuRoleFunctionalityApprovalProcess_FunctionalityApprovalProcess_FunctionalityApprovalProcessId",
                        column: x => x.FunctionalityApprovalProcessId,
                        principalTable: "FunctionalityApprovalProcess",
                        principalColumn: "FunctionalityApprovalProcessId");
                    table.ForeignKey(
                        name: "FK_MenuRoleFunctionalityApprovalProcess_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    SubCountyId = table.Column<int>(type: "int", nullable: true),
                    Latitude = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Longitude = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Location__E7FEA497CCA3DC99", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_Districts_Locations",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "DistrictId");
                    table.ForeignKey(
                        name: "FK_Regions_Locations",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId");
                    table.ForeignKey(
                        name: "FK_SubCounties_Locations",
                        column: x => x.SubCountyId,
                        principalTable: "SubCounties",
                        principalColumn: "SubCountyId");
                });

            migrationBuilder.CreateTable(
                name: "Locations_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    LocationName = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    SubCountyId = table.Column<int>(type: "int", nullable: true),
                    Latitude = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Longitude = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Location__5F3896384816BFE7", x => x.HistoryRowId);
                    table.ForeignKey(
                        name: "FK_Districts_Locations_AU",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "DistrictId");
                    table.ForeignKey(
                        name: "FK_Regions_Locations_AU",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId");
                    table.ForeignKey(
                        name: "FK_SubCounties_Locations_AU",
                        column: x => x.SubCountyId,
                        principalTable: "SubCounties",
                        principalColumn: "SubCountyId");
                });

            migrationBuilder.CreateTable(
                name: "Parishes",
                columns: table => new
                {
                    ParishId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParishName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SubCountyId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Parishes__9D996857AA716091", x => x.ParishId);
                    table.ForeignKey(
                        name: "FK_Parishes_SubCounties_SubCountyId",
                        column: x => x.SubCountyId,
                        principalTable: "SubCounties",
                        principalColumn: "SubCountyId");
                });

            migrationBuilder.CreateTable(
                name: "InsuranceRisk",
                columns: table => new
                {
                    InsuranceRiskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsurancePolicyId = table.Column<int>(type: "int", nullable: false),
                    SeasonId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    CropName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Deductible = table.Column<decimal>(type: "money", nullable: true),
                    DroughtLoss = table.Column<decimal>(type: "money", nullable: true),
                    ExcessRainfallLoss = table.Column<decimal>(type: "money", nullable: true),
                    TotalLoss = table.Column<decimal>(type: "money", nullable: true),
                    Payout = table.Column<decimal>(type: "money", nullable: true),
                    FinalPayout = table.Column<decimal>(type: "money", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Insuranc__1332B5FCCA81E9F2", x => x.InsuranceRiskId);
                    table.ForeignKey(
                        name: "FK_InsurancePolicy_InsuranceRisk",
                        column: x => x.InsurancePolicyId,
                        principalTable: "InsurancePolicies",
                        principalColumn: "InsurancePolicyId");
                    table.ForeignKey(
                        name: "FK_Locations_InsuranceRisk",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId");
                });

            migrationBuilder.CreateTable(
                name: "InsuranceRisk_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    InsuranceRiskId = table.Column<int>(type: "int", nullable: false),
                    InsurancePolicyId = table.Column<int>(type: "int", nullable: false),
                    SeasonId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    CropName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Deductible = table.Column<decimal>(type: "money", nullable: true),
                    DroughtLoss = table.Column<decimal>(type: "money", nullable: true),
                    ExcessRainfallLoss = table.Column<decimal>(type: "money", nullable: true),
                    TotalLoss = table.Column<decimal>(type: "money", nullable: true),
                    Payout = table.Column<decimal>(type: "money", nullable: true),
                    FinalPayout = table.Column<decimal>(type: "money", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Insuranc__5F38963872EEE764", x => x.HistoryRowId);
                    table.ForeignKey(
                        name: "FK_InsurancePolicy_InsuranceRisk_AU",
                        column: x => x.InsurancePolicyId,
                        principalTable: "InsurancePolicies",
                        principalColumn: "InsurancePolicyId");
                    table.ForeignKey(
                        name: "FK_Locations_InsuranceRisk_AU",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId");
                });

            migrationBuilder.CreateTable(
                name: "Programs",
                columns: table => new
                {
                    ProgramId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgramName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RegionId = table.Column<int>(type: "int", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: true),
                    SubCountyId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ParishId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Programs__752560587B88440F", x => x.ProgramId);
                    table.ForeignKey(
                        name: "FK_Programs_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "DistrictId");
                    table.ForeignKey(
                        name: "FK_Programs_Parishes_ParishId",
                        column: x => x.ParishId,
                        principalTable: "Parishes",
                        principalColumn: "ParishId");
                    table.ForeignKey(
                        name: "FK_Programs_Regions_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regions",
                        principalColumn: "RegionId");
                    table.ForeignKey(
                        name: "FK_Programs_SubCounties_SubCountyId",
                        column: x => x.SubCountyId,
                        principalTable: "SubCounties",
                        principalColumn: "SubCountyId");
                });

            migrationBuilder.CreateTable(
                name: "CropInsurance",
                columns: table => new
                {
                    CropInsuranceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FarmerId = table.Column<int>(type: "int", nullable: false),
                    FarmerCropId = table.Column<int>(type: "int", nullable: false),
                    CropName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(8,6)", nullable: true),
                    InsurancePolicyId = table.Column<int>(type: "int", nullable: false),
                    InsuranceRiskId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Comments = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CropInsu__61690A0DCA9B17A4", x => x.CropInsuranceId);
                    table.ForeignKey(
                        name: "FK_FarmerCrops_CropInsurance",
                        column: x => x.FarmerCropId,
                        principalTable: "FarmerCrops",
                        principalColumn: "FarmerCropId");
                    table.ForeignKey(
                        name: "FK_Farmers_CropInsurance",
                        column: x => x.FarmerId,
                        principalTable: "Farmers",
                        principalColumn: "FarmerId");
                    table.ForeignKey(
                        name: "FK_InsurancePolicies_CropInsurance",
                        column: x => x.InsurancePolicyId,
                        principalTable: "InsurancePolicies",
                        principalColumn: "InsurancePolicyId");
                    table.ForeignKey(
                        name: "FK_InsuranceRisk_CropInsurance",
                        column: x => x.InsuranceRiskId,
                        principalTable: "InsuranceRisk",
                        principalColumn: "InsuranceRiskId");
                });

            migrationBuilder.CreateTable(
                name: "CropInsurance_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CropInsuranceId = table.Column<int>(type: "int", nullable: false),
                    FarmerId = table.Column<int>(type: "int", nullable: false),
                    FarmerCropId = table.Column<int>(type: "int", nullable: false),
                    CropName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Longitude = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(8,6)", nullable: true),
                    InsurancePolicyId = table.Column<int>(type: "int", nullable: false),
                    InsuranceRiskId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Comments = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CropInsu__5F389638B262D123", x => x.HistoryRowId);
                    table.ForeignKey(
                        name: "FK_FarmerCrops_CropInsurance_AU",
                        column: x => x.FarmerCropId,
                        principalTable: "FarmerCrops",
                        principalColumn: "FarmerCropId");
                    table.ForeignKey(
                        name: "FK_Farmers_CropInsurance_AU",
                        column: x => x.FarmerId,
                        principalTable: "Farmers",
                        principalColumn: "FarmerId");
                    table.ForeignKey(
                        name: "FK_InsurancePolicies_CropInsurance_AU",
                        column: x => x.InsurancePolicyId,
                        principalTable: "InsurancePolicies",
                        principalColumn: "InsurancePolicyId");
                    table.ForeignKey(
                        name: "FK_InsuranceRisk_CropInsurance_AU",
                        column: x => x.InsuranceRiskId,
                        principalTable: "InsuranceRisk",
                        principalColumn: "InsuranceRiskId");
                });

            migrationBuilder.CreateTable(
                name: "InsurancePremium",
                columns: table => new
                {
                    InsurancePremiumId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsurancePolicyId = table.Column<int>(type: "int", nullable: false),
                    InsuranceRiskId = table.Column<int>(type: "int", nullable: true),
                    TotalPremiumAmount = table.Column<decimal>(type: "money", nullable: true),
                    PremiumAmountCurrency = table.Column<decimal>(type: "money", nullable: true),
                    SumInsuredAmount = table.Column<decimal>(type: "money", nullable: true),
                    SumInsuredAmountCurrency = table.Column<decimal>(type: "money", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Insuranc__F95457D5C446FE53", x => x.InsurancePremiumId);
                    table.ForeignKey(
                        name: "FK_InsurancePolicy_InsurancePremium",
                        column: x => x.InsurancePolicyId,
                        principalTable: "InsurancePolicies",
                        principalColumn: "InsurancePolicyId");
                    table.ForeignKey(
                        name: "FK_InsuranceRisk_InsurancePremium",
                        column: x => x.InsuranceRiskId,
                        principalTable: "InsuranceRisk",
                        principalColumn: "InsuranceRiskId");
                });

            migrationBuilder.CreateTable(
                name: "InsurancePremium_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    InsurancePremiumId = table.Column<int>(type: "int", nullable: false),
                    InsurancePolicyId = table.Column<int>(type: "int", nullable: false),
                    InsuranceRiskId = table.Column<int>(type: "int", nullable: true),
                    TotalPremiumAmount = table.Column<decimal>(type: "money", nullable: true),
                    PremiumAmountCurrency = table.Column<decimal>(type: "money", nullable: true),
                    SumInsuredAmount = table.Column<decimal>(type: "money", nullable: true),
                    SumInsuredAmountCurrency = table.Column<decimal>(type: "money", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Insuranc__5F389638C032938E", x => x.HistoryRowId);
                    table.ForeignKey(
                        name: "FK_InsurancePolicy_InsurancePremium_AU",
                        column: x => x.InsurancePolicyId,
                        principalTable: "InsurancePolicies",
                        principalColumn: "InsurancePolicyId");
                    table.ForeignKey(
                        name: "FK_InsuranceRisk_InsurancePremium_AU",
                        column: x => x.InsuranceRiskId,
                        principalTable: "InsuranceRisk",
                        principalColumn: "InsuranceRiskId");
                });

            migrationBuilder.CreateTable(
                name: "InsurancePremiumFrequency",
                columns: table => new
                {
                    PremiumFrequencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsurancePremiumId = table.Column<int>(type: "int", nullable: false),
                    PremiumFrequency = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Insuranc__14EBA19CB4DDCBAF", x => x.PremiumFrequencyId);
                    table.ForeignKey(
                        name: "FK_InsurancePremium_InsurancePremiumFrequency",
                        column: x => x.InsurancePremiumId,
                        principalTable: "InsurancePremium",
                        principalColumn: "InsurancePremiumId");
                });

            migrationBuilder.CreateTable(
                name: "InsurancePremiumFrequency_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    PremiumFrequencyId = table.Column<int>(type: "int", nullable: false),
                    InsurancePremiumId = table.Column<int>(type: "int", nullable: false),
                    PremiumFrequency = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Insuranc__5F389638E1F69167", x => x.HistoryRowId);
                    table.ForeignKey(
                        name: "FK_InsurancePremium_InsurancePremiumFrequency_AU",
                        column: x => x.InsurancePremiumId,
                        principalTable: "InsurancePremium",
                        principalColumn: "InsurancePremiumId");
                });

            migrationBuilder.CreateTable(
                name: "CropInsurancePremiums",
                columns: table => new
                {
                    CropInsurancePremiumId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CropInsuranceId = table.Column<int>(type: "int", nullable: true),
                    InsurancePremiumId = table.Column<int>(type: "int", nullable: true),
                    PremiumFrequencyId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CropInsu__EB982450150CD06B", x => x.CropInsurancePremiumId);
                    table.ForeignKey(
                        name: "FK_CropInsurance_CropInsurancePremiums",
                        column: x => x.CropInsuranceId,
                        principalTable: "CropInsurance",
                        principalColumn: "CropInsuranceId");
                    table.ForeignKey(
                        name: "FK_InsurancePremium_CropInsurancePremiums",
                        column: x => x.InsurancePremiumId,
                        principalTable: "InsurancePremium",
                        principalColumn: "InsurancePremiumId");
                    table.ForeignKey(
                        name: "FK_PremiumFrequency_CropInsurancePremiums",
                        column: x => x.PremiumFrequencyId,
                        principalTable: "InsurancePremiumFrequency",
                        principalColumn: "PremiumFrequencyId");
                });

            migrationBuilder.CreateTable(
                name: "CropInsurancePremiums_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CropInsurancePremiumId = table.Column<int>(type: "int", nullable: false),
                    CropInsuranceId = table.Column<int>(type: "int", nullable: true),
                    InsurancePremiumId = table.Column<int>(type: "int", nullable: true),
                    PremiumFrequencyId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CropInsu__5F3896381AF0D3F9", x => x.HistoryRowId);
                    table.ForeignKey(
                        name: "FK_CropInsurance_CropInsurancePremiums_AU",
                        column: x => x.CropInsuranceId,
                        principalTable: "CropInsurance",
                        principalColumn: "CropInsuranceId");
                    table.ForeignKey(
                        name: "FK_InsurancePremium_CropInsurancePremiums_AU",
                        column: x => x.InsurancePremiumId,
                        principalTable: "InsurancePremium",
                        principalColumn: "InsurancePremiumId");
                    table.ForeignKey(
                        name: "FK_PremiumFrequency_CropInsurancePremiums_AU",
                        column: x => x.PremiumFrequencyId,
                        principalTable: "InsurancePremiumFrequency",
                        principalColumn: "PremiumFrequencyId");
                });

            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    ClaimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    FarmerId = table.Column<int>(type: "int", nullable: true),
                    CropInsuranceId = table.Column<int>(type: "int", nullable: true),
                    CropInsurancePremiumId = table.Column<int>(type: "int", nullable: true),
                    RequestedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    AllowedAmount = table.Column<decimal>(type: "money", nullable: true),
                    DisallowAmount = table.Column<decimal>(type: "money", nullable: true),
                    OtherChargesAmount = table.Column<decimal>(type: "money", nullable: true),
                    Currency = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PaidDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Claims__EF2E139B6F0F8FA1", x => x.ClaimId);
                    table.ForeignKey(
                        name: "FK_CropInsurancePremiums_Claims",
                        column: x => x.CropInsurancePremiumId,
                        principalTable: "CropInsurancePremiums",
                        principalColumn: "CropInsurancePremiumId");
                    table.ForeignKey(
                        name: "FK_CropInsurances_Claims",
                        column: x => x.CropInsuranceId,
                        principalTable: "CropInsurance",
                        principalColumn: "CropInsuranceId");
                    table.ForeignKey(
                        name: "FK_Farmers_Claims",
                        column: x => x.FarmerId,
                        principalTable: "Farmers",
                        principalColumn: "FarmerId");
                });

            migrationBuilder.CreateTable(
                name: "Claims_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    ClaimNumber = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    FarmerId = table.Column<int>(type: "int", nullable: true),
                    CropInsuranceId = table.Column<int>(type: "int", nullable: true),
                    CropInsurancePremiumId = table.Column<int>(type: "int", nullable: true),
                    RequestedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    AllowedAmount = table.Column<decimal>(type: "money", nullable: true),
                    DisallowAmount = table.Column<decimal>(type: "money", nullable: true),
                    OtherChargesAmount = table.Column<decimal>(type: "money", nullable: true),
                    Currency = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PaidDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Status = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Claims_A__5F3896387C46B954", x => x.HistoryRowId);
                    table.ForeignKey(
                        name: "FK_CropInsurancePremiums_Claims_AU",
                        column: x => x.CropInsurancePremiumId,
                        principalTable: "CropInsurancePremiums",
                        principalColumn: "CropInsurancePremiumId");
                    table.ForeignKey(
                        name: "FK_CropInsurances_Claims_AU",
                        column: x => x.CropInsuranceId,
                        principalTable: "CropInsurance",
                        principalColumn: "CropInsuranceId");
                    table.ForeignKey(
                        name: "FK_Farmers_Claims_AU",
                        column: x => x.FarmerId,
                        principalTable: "Farmers",
                        principalColumn: "FarmerId");
                });

            migrationBuilder.CreateTable(
                name: "PremiumPayments",
                columns: table => new
                {
                    PremiumPaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CropInsuranceId = table.Column<int>(type: "int", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "money", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "money", nullable: true),
                    TotalPaidAmount = table.Column<decimal>(type: "money", nullable: true),
                    Currency = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ModeOfPayment = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PremiumP__679D297815A2DF98", x => x.PremiumPaymentId);
                    table.ForeignKey(
                        name: "FK_CropInsurance_PremiumPayments",
                        column: x => x.CropInsuranceId,
                        principalTable: "CropInsurance",
                        principalColumn: "CropInsuranceId");
                });

            migrationBuilder.CreateTable(
                name: "PremiumPayments_AU",
                columns: table => new
                {
                    HistoryRowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HistoryCreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    PremiumPaymentId = table.Column<int>(type: "int", nullable: false),
                    CropInsuranceId = table.Column<int>(type: "int", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PaidAmount = table.Column<decimal>(type: "money", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "money", nullable: true),
                    TotalPaidAmount = table.Column<decimal>(type: "money", nullable: true),
                    Currency = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ModeOfPayment = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Status = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ModifiedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PremiumP__5F38963879046800", x => x.HistoryRowId);
                    table.ForeignKey(
                        name: "FK_CropInsurance_PremiumPayments_AU",
                        column: x => x.CropInsuranceId,
                        principalTable: "CropInsurance",
                        principalColumn: "CropInsuranceId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationChildMenu_ApplicationMenuId",
                table: "ApplicationChildMenu",
                column: "ApplicationMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationFunctionalities_ApplicationChildMenuId",
                table: "ApplicationFunctionalities",
                column: "ApplicationChildMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationFunctionalities_ApplicationMenuId",
                table: "ApplicationFunctionalities",
                column: "ApplicationMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_CropInsuranceId",
                table: "Claims",
                column: "CropInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_FarmerId",
                table: "Claims",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_AU_CropInsuranceId",
                table: "Claims_AU",
                column: "CropInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Claims_AU_FarmerId",
                table: "Claims_AU",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_CropInsurance_FarmerCropId",
                table: "CropInsurance",
                column: "FarmerCropId");

            migrationBuilder.CreateIndex(
                name: "IX_CropInsurance_FarmerId",
                table: "CropInsurance",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_CropInsurance_InsurancePolicyId",
                table: "CropInsurance",
                column: "InsurancePolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_CropInsurance_InsuranceRiskId",
                table: "CropInsurance",
                column: "InsuranceRiskId");

            migrationBuilder.CreateIndex(
                name: "IX_CropInsurance_AU_FarmerCropId",
                table: "CropInsurance_AU",
                column: "FarmerCropId");

            migrationBuilder.CreateIndex(
                name: "IX_CropInsurance_AU_FarmerId",
                table: "CropInsurance_AU",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_CropInsurance_AU_InsurancePolicyId",
                table: "CropInsurance_AU",
                column: "InsurancePolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_CropInsurance_AU_InsuranceRiskId",
                table: "CropInsurance_AU",
                column: "InsuranceRiskId");

            migrationBuilder.CreateIndex(
                name: "IX_CropInsuranceCategory_CompanyId",
                table: "CropInsuranceCategory",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CropInsurancePremiums_CropInsuranceId",
                table: "CropInsurancePremiums",
                column: "CropInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_CropInsurancePremiums_InsurancePremiumId",
                table: "CropInsurancePremiums",
                column: "InsurancePremiumId");

            migrationBuilder.CreateIndex(
                name: "IX_CropInsurancePremiums_PremiumFrequencyId",
                table: "CropInsurancePremiums",
                column: "PremiumFrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CropInsurancePremiums_AU_CropInsuranceId",
                table: "CropInsurancePremiums_AU",
                column: "CropInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_CropInsurancePremiums_AU_InsurancePremiumId",
                table: "CropInsurancePremiums_AU",
                column: "InsurancePremiumId");

            migrationBuilder.CreateIndex(
                name: "IX_CropInsurancePremiums_AU_PremiumFrequencyId",
                table: "CropInsurancePremiums_AU",
                column: "PremiumFrequencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Crops_CropCategoryId",
                table: "Crops",
                column: "CropCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Crops_AU_CropCategoryId",
                table: "Crops_AU",
                column: "CropCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_RegionId",
                table: "Districts",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_AU_RegionId",
                table: "Districts_AU",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_EventId",
                table: "EmailTemplates",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_RoleId",
                table: "EmailTemplates",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_AU_EventId",
                table: "EmailTemplates_AU",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplates_AU_RoleId",
                table: "EmailTemplates_AU",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmerCrops_CropId",
                table: "FarmerCrops",
                column: "CropId");

            migrationBuilder.CreateIndex(
                name: "IX_FarmerCrops_FarmerId",
                table: "FarmerCrops",
                column: "FarmerId");

            migrationBuilder.CreateIndex(
                name: "IX_Functionalities_FeatureId",
                table: "Functionalities",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_Functionalities_AU_FeatureId",
                table: "Functionalities_AU",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionalityApprovalProcess_ApplicationFunctionalityId",
                table: "FunctionalityApprovalProcess",
                column: "ApplicationFunctionalityId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_InsuranceProviderId",
                table: "InsurancePolicies",
                column: "InsuranceProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicies_AU_InsuranceProviderId",
                table: "InsurancePolicies_AU",
                column: "InsuranceProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicy_CategoryId",
                table: "InsurancePolicy",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePolicy_CompanyId",
                table: "InsurancePolicy",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePremium_InsurancePolicyId",
                table: "InsurancePremium",
                column: "InsurancePolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePremium_InsuranceRiskId",
                table: "InsurancePremium",
                column: "InsuranceRiskId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePremium_AU_InsurancePolicyId",
                table: "InsurancePremium_AU",
                column: "InsurancePolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePremium_AU_InsuranceRiskId",
                table: "InsurancePremium_AU",
                column: "InsuranceRiskId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePremiumFrequency_InsurancePremiumId",
                table: "InsurancePremiumFrequency",
                column: "InsurancePremiumId");

            migrationBuilder.CreateIndex(
                name: "IX_InsurancePremiumFrequency_AU_InsurancePremiumId",
                table: "InsurancePremiumFrequency_AU",
                column: "InsurancePremiumId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceProviderDocuments_InsurerId",
                table: "InsuranceProviderDocuments",
                column: "InsurerId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceProviderDocuments_AU_InsurerId",
                table: "InsuranceProviderDocuments_AU",
                column: "InsurerId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceProviders_CountryId",
                table: "InsuranceProviders",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceProviders_AU_CountryId",
                table: "InsuranceProviders_AU",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceRisk_InsurancePolicyId",
                table: "InsuranceRisk",
                column: "InsurancePolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceRisk_LocationId",
                table: "InsuranceRisk",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceRisk_AU_InsurancePolicyId",
                table: "InsuranceRisk_AU",
                column: "InsurancePolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceRisk_AU_LocationId",
                table: "InsuranceRisk_AU",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_DistrictId",
                table: "Locations",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_RegionId",
                table: "Locations",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_SubCountyId",
                table: "Locations",
                column: "SubCountyId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AU_DistrictId",
                table: "Locations_AU",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AU_RegionId",
                table: "Locations_AU",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AU_SubCountyId",
                table: "Locations_AU",
                column: "SubCountyId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRoleFunctionalityApprovalProcess_FunctionalityApprovalProcessId",
                table: "MenuRoleFunctionalityApprovalProcess",
                column: "FunctionalityApprovalProcessId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRoleFunctionalityApprovalProcess_RoleId",
                table: "MenuRoleFunctionalityApprovalProcess",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRolesFunctionalities_ApplicationFunctionalityId",
                table: "MenuRolesFunctionalities",
                column: "ApplicationFunctionalityId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRolesFunctionalities_RoleId",
                table: "MenuRolesFunctionalities",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRolesPrivileges_ApplicationChildMenuId",
                table: "MenuRolesPrivileges",
                column: "ApplicationChildMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRolesPrivileges_ApplicationMenuId",
                table: "MenuRolesPrivileges",
                column: "ApplicationMenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuRolesPrivileges_RoleId",
                table: "MenuRolesPrivileges",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Parishes_SubCountyId",
                table: "Parishes",
                column: "SubCountyId");

            migrationBuilder.CreateIndex(
                name: "IX_PremiumPayments_CropInsuranceId",
                table: "PremiumPayments",
                column: "CropInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_PremiumPayments_AU_CropInsuranceId",
                table: "PremiumPayments_AU",
                column: "CropInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_DistrictId",
                table: "Programs",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_ParishId",
                table: "Programs",
                column: "ParishId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_RegionId",
                table: "Programs",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_SubCountyId",
                table: "Programs",
                column: "SubCountyId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_CountryId",
                table: "Regions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Regions_AU_CountryId",
                table: "Regions_AU",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ReportingToId",
                table: "Roles",
                column: "ReportingToId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonCutOffDates_CropCategoryId",
                table: "SeasonCutOffDates",
                column: "CropCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonCutOffDates_CropId",
                table: "SeasonCutOffDates",
                column: "CropId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonCutOffDates_RegionId",
                table: "SeasonCutOffDates",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_SeasonCutOffDates_SeasonId",
                table: "SeasonCutOffDates",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCounties_DistrictId",
                table: "SubCounties",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCounties_AU_DistrictId",
                table: "SubCounties_AU",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_SubFunctionalities_FunctionalityId",
                table: "SubFunctionalities",
                column: "FunctionalityId");

            migrationBuilder.CreateIndex(
                name: "IX_SubFunctionalities_AU_FunctionalityId",
                table: "SubFunctionalities_AU",
                column: "FunctionalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_InsurerId",
                table: "Users",
                column: "InsurerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ReportingTo",
                table: "Users",
                column: "ReportingTo");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppEvents_AU");

            migrationBuilder.DropTable(
                name: "ApplicationSettings");

            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "Claims_AU");

            migrationBuilder.DropTable(
                name: "Countries_AU");

            migrationBuilder.DropTable(
                name: "CropCategories_AU");

            migrationBuilder.DropTable(
                name: "CropInsurance_AU");

            migrationBuilder.DropTable(
                name: "CropInsuranceCategory_Au");

            migrationBuilder.DropTable(
                name: "CropInsurancePremiums_AU");

            migrationBuilder.DropTable(
                name: "Crops_AU");

            migrationBuilder.DropTable(
                name: "Districts_AU");

            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "EmailTemplates_AU");

            migrationBuilder.DropTable(
                name: "EsusFarmPolicy");

            migrationBuilder.DropTable(
                name: "FarmerCrops_AU");

            migrationBuilder.DropTable(
                name: "Farmers_AU");

            migrationBuilder.DropTable(
                name: "Functionalities_AU");

            migrationBuilder.DropTable(
                name: "InsuranceCompany_Au");

            migrationBuilder.DropTable(
                name: "InsurancePolicies_AU");

            migrationBuilder.DropTable(
                name: "InsurancePolicy");

            migrationBuilder.DropTable(
                name: "InsurancePolicy_Au");

            migrationBuilder.DropTable(
                name: "InsurancePremium_AU");

            migrationBuilder.DropTable(
                name: "InsurancePremiumFrequency_AU");

            migrationBuilder.DropTable(
                name: "InsuranceProviderDocuments");

            migrationBuilder.DropTable(
                name: "InsuranceProviderDocuments_AU");

            migrationBuilder.DropTable(
                name: "InsuranceProviders_AU");

            migrationBuilder.DropTable(
                name: "InsuranceRequests");

            migrationBuilder.DropTable(
                name: "InsuranceRisk_AU");

            migrationBuilder.DropTable(
                name: "Locations_AU");

            migrationBuilder.DropTable(
                name: "MenuRoleFunctionalityApprovalProcess");

            migrationBuilder.DropTable(
                name: "MenuRolesFunctionalities");

            migrationBuilder.DropTable(
                name: "MenuRolesPrivileges");

            migrationBuilder.DropTable(
                name: "PaymentModes");

            migrationBuilder.DropTable(
                name: "PaymentModes_AU");

            migrationBuilder.DropTable(
                name: "PremiumPayments");

            migrationBuilder.DropTable(
                name: "PremiumPayments_AU");

            migrationBuilder.DropTable(
                name: "Programs");

            migrationBuilder.DropTable(
                name: "Regions_AU");

            migrationBuilder.DropTable(
                name: "SeasonCutOffDates");

            migrationBuilder.DropTable(
                name: "SubCounties_AU");

            migrationBuilder.DropTable(
                name: "SubFunctionalities");

            migrationBuilder.DropTable(
                name: "SubFunctionalities_AU");

            migrationBuilder.DropTable(
                name: "TaxComponents");

            migrationBuilder.DropTable(
                name: "TaxComponents_AU");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "AppEvents");

            migrationBuilder.DropTable(
                name: "CropInsuranceCategory");

            migrationBuilder.DropTable(
                name: "FunctionalityApprovalProcess");

            migrationBuilder.DropTable(
                name: "CropInsurancePremiums");

            migrationBuilder.DropTable(
                name: "Parishes");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "Functionalities");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "InsuranceCompany");

            migrationBuilder.DropTable(
                name: "ApplicationFunctionalities");

            migrationBuilder.DropTable(
                name: "CropInsurance");

            migrationBuilder.DropTable(
                name: "InsurancePremiumFrequency");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "ApplicationChildMenu");

            migrationBuilder.DropTable(
                name: "FarmerCrops");

            migrationBuilder.DropTable(
                name: "InsurancePremium");

            migrationBuilder.DropTable(
                name: "ApplicationMenu");

            migrationBuilder.DropTable(
                name: "Crops");

            migrationBuilder.DropTable(
                name: "Farmers");

            migrationBuilder.DropTable(
                name: "InsuranceRisk");

            migrationBuilder.DropTable(
                name: "CropCategories");

            migrationBuilder.DropTable(
                name: "InsurancePolicies");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "InsuranceProviders");

            migrationBuilder.DropTable(
                name: "SubCounties");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}

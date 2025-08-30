using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OpenProfileAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AuthPolicy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    Enforce2FactorVerification = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EnforcePasswordChangeOnFirstLogin = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EnforceBackendActivation = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EnforceEmailConfirmation = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EnforceMobileConfirmation = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EnforceProfileCompletion = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BaseTokenDurationMinutes = table.Column<int>(type: "int", nullable: false),
                    FullTokenDurationMinutes = table.Column<int>(type: "int", nullable: false),
                    RefreshTokenDurationMinutes = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthPolicy", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EntityType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EntityTypeParentId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityType", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FileStore",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FilePath = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ContentType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AWSBucketName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FileSize = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileStore", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Right",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Right", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SubscriptionPlan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StripePriceId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MonthlyPrice = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    MaxApplicationsPerMonth = table.Column<int>(type: "int", nullable: false),
                    MaxApplicationsPerDay = table.Column<int>(type: "int", nullable: false),
                    IsDefault = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlan", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastLoginDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    PasswordChangedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UserTypeId = table.Column<int>(type: "int", nullable: false),
                    AuthKey = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SmsKey = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailKey = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsProfileCompleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LastLoginAttempt = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DisplayOrder = table.Column<int>(type: "int", nullable: true),
                    ColorCode = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EntityTypeId = table.Column<int>(type: "int", nullable: false),
                    ParentCategoryId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_EntityType_EntityTypeId",
                        column: x => x.EntityTypeId,
                        principalTable: "EntityType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoleRight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    RightId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleRight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleRight_Right_RightId",
                        column: x => x.RightId,
                        principalTable: "Right",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoleRight_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Education",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Degree = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Institution = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FieldOfStudy = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Education", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Education_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Experience",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CompanyName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Position = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Location = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    IsCurrent = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experience_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "LoginAttempts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RejectionType = table.Column<int>(type: "int", nullable: false),
                    LoginType = table.Column<int>(type: "int", nullable: false),
                    AttemptDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    IsSuccess = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IpAddress = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClientInformation = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TwoFacVerified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeviceRegistration = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoginAttempts_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    ProficiencyLevelId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Skill_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SubscriptionPlanId = table.Column<int>(type: "int", nullable: false),
                    StripeCustomerId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StripeSubscriptionId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StartedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RenewedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CanceledAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscription_SubscriptionPlan_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscription_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FileStoreId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    SubCategoryId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserFile_FileStore_FileStoreId",
                        column: x => x.FileStoreId,
                        principalTable: "FileStore",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFile_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserJobApplication",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ExternalJobId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JobTitle = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CompanyName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    AppliedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserJobApplication", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserJobApplication_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserJobPreference",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    JobTitle = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IndustryId = table.Column<int>(type: "int", nullable: false),
                    WorkModeId = table.Column<int>(type: "int", nullable: false),
                    SalaryRangeMin = table.Column<int>(type: "int", nullable: false),
                    SalaryRangeMax = table.Column<int>(type: "int", nullable: false),
                    CompanySize = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JobTypeId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserJobPreference", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserJobPreference_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserRight",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RightId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRight", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRight_Right_RightId",
                        column: x => x.RightId,
                        principalTable: "Right",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserRight_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserProfile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DisplayName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MobileNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    PostalCode = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    GenderId = table.Column<int>(type: "int", nullable: false),
                    NationalId = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    MaritalStatusId = table.Column<int>(type: "int", nullable: true),
                    Occupation = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LinkedInProfile = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserBio = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Created = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: true),
                    Comment = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfile_Category_CityId",
                        column: x => x.CityId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserProfile_Category_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserProfile_Category_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfile_Category_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserProfile_Category_StateId",
                        column: x => x.StateId,
                        principalTable: "Category",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserProfile_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Category_EntityTypeId",
                table: "Category",
                column: "EntityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Education_UserId",
                table: "Education",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_UserId",
                table: "Experience",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LoginAttempts_UserId",
                table: "LoginAttempts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleRight_RightId",
                table: "RoleRight",
                column: "RightId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleRight_RoleId",
                table: "RoleRight",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_UserId",
                table: "Skill",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_SubscriptionPlanId",
                table: "Subscription",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_UserId",
                table: "Subscription",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "User",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "User",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFile_FileStoreId",
                table: "UserFile",
                column: "FileStoreId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFile_UserId",
                table: "UserFile",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserJobApplication_UserId",
                table: "UserJobApplication",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserJobPreference_UserId",
                table: "UserJobPreference",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_CityId",
                table: "UserProfile",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_CountryId",
                table: "UserProfile",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_GenderId",
                table: "UserProfile",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_MaritalStatusId",
                table: "UserProfile",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_StateId",
                table: "UserProfile",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_UserId",
                table: "UserProfile",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRight_RightId",
                table: "UserRight",
                column: "RightId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRight_UserId",
                table: "UserRight",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthPolicy");

            migrationBuilder.DropTable(
                name: "Education");

            migrationBuilder.DropTable(
                name: "Experience");

            migrationBuilder.DropTable(
                name: "LoginAttempts");

            migrationBuilder.DropTable(
                name: "RoleRight");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "UserFile");

            migrationBuilder.DropTable(
                name: "UserJobApplication");

            migrationBuilder.DropTable(
                name: "UserJobPreference");

            migrationBuilder.DropTable(
                name: "UserProfile");

            migrationBuilder.DropTable(
                name: "UserRight");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "SubscriptionPlan");

            migrationBuilder.DropTable(
                name: "FileStore");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Right");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "EntityType");
        }
    }
}

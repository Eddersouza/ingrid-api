using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IP.AccIPInfo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "AccIPInfo");

            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AccountsIP",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Alias_Value = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Alias_ValueNormalized = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    BusinessBranchSegment_BranchId = table.Column<Guid>(type: "char(36)", nullable: true),
                    BusinessBranchSegment_BranchName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    BusinessBranchSegment_SegmentId = table.Column<Guid>(type: "char(36)", nullable: true),
                    BusinessBranchSegment_SegmentName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Customer_Id = table.Column<Guid>(type: "char(36)", nullable: true),
                    Customer_Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Customer_NameNormalized = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Integrator_Id = table.Column<Guid>(type: "char(36)", nullable: true),
                    Integrator_Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Integrator_NameNormalized = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    Owner_OwnerIsIP = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    Owner_Id = table.Column<Guid>(type: "char(36)", nullable: true),
                    Owner_Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Owner_NameNormalized = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Retailer_Id = table.Column<Guid>(type: "char(36)", nullable: true),
                    Retailer_Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Retailer_NameNormalized = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    StatusCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Subscription_Id = table.Column<Guid>(type: "char(36)", nullable: true),
                    Subscription_Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Subscription_NameNormalized = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    TypeCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    AuditableInfo_CreatedBy = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    AuditableInfo_CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AuditableInfo_UpdatedBy = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    AuditableInfo_UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletableInfo_Deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeletableInfo_DeletedReason = table.Column<string>(type: "text(5000)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsIP", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AccountSubscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name_ValueNormalized = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Name_Value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ExternalId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    ActivableInfo_Active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    ActivableInfo_InativeReason = table.Column<string>(type: "text(5000)", maxLength: 100, nullable: true),
                    AuditableInfo_CreatedBy = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    AuditableInfo_CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AuditableInfo_UpdatedBy = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    AuditableInfo_UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletableInfo_Deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeletableInfo_DeletedReason = table.Column<string>(type: "text(5000)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountSubscriptions", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AuditTrails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    AffectedColumns = table.Column<string>(type: "longtext", maxLength: 100, nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    NewValues = table.Column<string>(type: "longtext", maxLength: 100, nullable: true),
                    OldValues = table.Column<string>(type: "longtext", maxLength: 100, nullable: true),
                    PrimaryKey = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    RequestId = table.Column<Guid>(type: "char(36)", nullable: false),
                    TableName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrails", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "IntegratorSystems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name_ValueNormalized = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Name_Value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    SiteUrl = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    ActivableInfo_Active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    ActivableInfo_InativeReason = table.Column<string>(type: "text(5000)", maxLength: 100, nullable: true),
                    AuditableInfo_CreatedBy = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    AuditableInfo_CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AuditableInfo_UpdatedBy = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    AuditableInfo_UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletableInfo_Deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeletableInfo_DeletedReason = table.Column<string>(type: "text(5000)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegratorSystems", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AccountsIPMovementsSummary",
                schema: "AccIPInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    AccountIPId = table.Column<Guid>(type: "char(36)", nullable: false),
                    AccountNumber = table.Column<int>(type: "int", nullable: false),
                    CancelAmount = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    CancelQuantity = table.Column<int>(type: "int", nullable: false),
                    MovementDateHour = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ReturnAmount = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    ReturnAmountParcial = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    ReturnQuantity = table.Column<int>(type: "int", nullable: false),
                    ReturnQuantityParcial = table.Column<int>(type: "int", nullable: false),
                    SettledAmount = table.Column<decimal>(type: "decimal(12,2)", precision: 12, scale: 2, nullable: false),
                    SettledQuantity = table.Column<int>(type: "int", nullable: false),
                    AuditableInfo_CreatedBy = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    AuditableInfo_CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AuditableInfo_UpdatedBy = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    AuditableInfo_UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountsIPMovementsSummary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountsIPMovementsSummary_AccountsIP_AccountIPId",
                        column: x => x.AccountIPId,
                        principalTable: "AccountsIP",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AccountsIPMovementsSummary_AccountIPId",
                schema: "AccIPInfo",
                table: "AccountsIPMovementsSummary",
                column: "AccountIPId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountsIPMovementsSummary",
                schema: "AccIPInfo");

            migrationBuilder.DropTable(
                name: "AccountSubscriptions");

            migrationBuilder.DropTable(
                name: "AuditTrails");

            migrationBuilder.DropTable(
                name: "IntegratorSystems");

            migrationBuilder.DropTable(
                name: "AccountsIP");
        }
    }
}

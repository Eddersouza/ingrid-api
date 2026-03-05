using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IP.Core.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
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
                name: "BusinessBranches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name_ValueNormalized = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Name_Value = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_BusinessBranches", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    DocumentNumber_Value = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false),
                    ExternalId = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Name_Value = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Name_ValueNormalized = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    PersonTypeCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Remarks = table.Column<string>(type: "text(5000)", maxLength: 100, nullable: true),
                    StatusCode = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    TradingName_Value = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    TradingName_ValueNormalized = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    AuditableInfo_CreatedBy = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    AuditableInfo_CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AuditableInfo_UpdatedBy = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    AuditableInfo_UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletableInfo_Deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeletableInfo_DeletedReason = table.Column<string>(type: "text(5000)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    CPF_Value = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: false),
                    Manager_Id = table.Column<Guid>(type: "char(36)", nullable: true),
                    Manager_Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Manager_NameNormalized = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Name_Value = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    Name_ValueNormalized = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    User_Id = table.Column<Guid>(type: "char(36)", nullable: true),
                    User_Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    User_NameNormalized = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
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
                    table.PrimaryKey("PK_Employees", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    IBGECode = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false),
                    Code = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false),
                    Name_ValueNormalized = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Name_Value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    AuditableInfo_CreatedBy = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    AuditableInfo_CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AuditableInfo_UpdatedBy = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    AuditableInfo_UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    DeletableInfo_Deleted = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false),
                    DeletableInfo_DeletedReason = table.Column<string>(type: "text(5000)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BusinessSegments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    BusinessBranchId = table.Column<Guid>(type: "char(36)", nullable: false),
                    SegmentName_Value = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    SegmentName_ValueNormalized = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_BusinessSegments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessSegments_BusinessBranches_BusinessBranchId",
                        column: x => x.BusinessBranchId,
                        principalTable: "BusinessBranches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    IBGECode = table.Column<string>(type: "varchar(7)", maxLength: 7, nullable: false),
                    Name_ValueNormalized = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Name_Value = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    StateId = table.Column<Guid>(type: "char(36)", nullable: false),
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
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Name_ValueNormalized = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Name_Value = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Neighborhood_ValueNormalized = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Neighborhood_Value = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    CityId = table.Column<Guid>(type: "char(36)", nullable: false),
                    ActivableInfo_Active = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    ActivableInfo_InativeReason = table.Column<string>(type: "text(5000)", maxLength: 100, nullable: true),
                    AuditableInfo_CreatedBy = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    AuditableInfo_CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AuditableInfo_UpdatedBy = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    AuditableInfo_UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CityId",
                table: "Addresses",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessSegments_BusinessBranchId",
                table: "BusinessSegments",
                column: "BusinessBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_StateId",
                table: "Cities",
                column: "StateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "AuditTrails");

            migrationBuilder.DropTable(
                name: "BusinessSegments");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "BusinessBranches");

            migrationBuilder.DropTable(
                name: "States");
        }
    }
}

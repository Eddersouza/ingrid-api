using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IP.Com.Persistence.Migrations
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
                name: "EmailsSchedule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Attempts = table.Column<int>(type: "int", nullable: false),
                    Body = table.Column<string>(type: "longtext", maxLength: 100, nullable: false),
                    CarbonCopy = table.Column<string>(type: "longtext", maxLength: 100, nullable: true),
                    Copy = table.Column<string>(type: "longtext", maxLength: 100, nullable: true),
                    ErrorMessages = table.Column<string>(type: "longtext", maxLength: 100, nullable: true),
                    Recipient = table.Column<string>(type: "longtext", maxLength: 100, nullable: false),
                    Sended = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Sender = table.Column<string>(type: "longtext", maxLength: 100, nullable: false),
                    Subject = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    LastAttemptDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    AuditableInfo_CreatedBy = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false),
                    AuditableInfo_CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    AuditableInfo_UpdatedBy = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    AuditableInfo_UpdatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailsSchedule", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTrails");

            migrationBuilder.DropTable(
                name: "EmailsSchedule");
        }
    }
}

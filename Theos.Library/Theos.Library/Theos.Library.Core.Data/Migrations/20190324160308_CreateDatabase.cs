using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Theos.Library.Core.Data.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookKey",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookKey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    OriginId = table.Column<Guid>(nullable: false),
                    Table = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Field = table.Column<string>(type: "VARCHAR(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "VARCHAR(MAX)", maxLength: 8000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserKey",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserKey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    KeyId = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Author = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(500)", maxLength: 500, nullable: false),
                    Image = table.Column<string>(type: "VARCHAR(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Book_BookKey_KeyId",
                        column: x => x.KeyId,
                        principalTable: "BookKey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    Active = table.Column<bool>(nullable: false),
                    KeyId = table.Column<Guid>(nullable: false),
                    Login = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    Password = table.Column<string>(type: "VARCHAR(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_UserKey_KeyId",
                        column: x => x.KeyId,
                        principalTable: "UserKey",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_KeyId",
                table: "Book",
                column: "KeyId");

            migrationBuilder.CreateIndex(
                name: "IX_User_KeyId",
                table: "User",
                column: "KeyId");

            migrationBuilder.Sql("INSERT INTO USERKEY (ID) VALUES ('6D1FC865-8DA6-44F7-9100-7276C570BDA3');");
            migrationBuilder.Sql("INSERT INTO[USER] (ID, DATE, USERID, VERSION, ACTIVE, KEYID, LOGIN, PASSWORD) values (NEWID(), GETDATE(), '00000000-0000-0000-0000-000000000000', 1, 1, '6D1FC865-8DA6-44F7-9100-7276C570BDA3', 'admin', 'ILdc/0VG9DUQLdL7hlN+akQi9ttqh9uPSteH0RkHpxA=');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "BookKey");

            migrationBuilder.DropTable(
                name: "UserKey");
        }
    }
}

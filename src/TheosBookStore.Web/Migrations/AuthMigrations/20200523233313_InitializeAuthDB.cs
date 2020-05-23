using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TheosBookStore.Web.Migrations.AuthMigrations
{
    public partial class InitializeAuthDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Hash = table.Column<byte[]>(nullable: false),
                    Salt = table.Column<byte[]>(nullable: false),
                    Roles = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "Id", "Email", "Hash", "Name", "Roles", "Salt" },
                values: new object[] { 1, "admin@admin.com", new byte[] { 109, 174, 249, 58, 86, 61, 216, 4, 22, 28, 141, 209, 29, 194, 82, 111, 229, 211, 117, 17, 19, 27, 219, 246, 198, 182, 213, 160, 129, 77, 185, 185, 166, 148, 149, 106, 200, 36, 248, 206, 196, 158, 176, 50, 151, 75, 162, 188, 129, 246, 253, 204, 96, 48, 128, 113, 80, 232, 2, 159, 43, 135, 92, 217 }, "admin", "ADMIN", new byte[] { 251, 190, 83, 17, 91, 147, 208, 224, 96, 129, 209, 80, 188, 23, 181, 11, 255, 120, 239, 39, 99, 208, 133, 100, 158, 67, 146, 217, 216, 189, 44, 218, 153, 145, 81, 40, 161, 219, 81, 116, 31, 195, 85, 251, 93, 164, 237, 56, 11, 128, 3, 226, 75, 169, 129, 229, 165, 206, 163, 45, 70, 33, 52, 188, 47, 101, 82, 6, 241, 116, 70, 141, 163, 139, 50, 106, 236, 209, 202, 13, 129, 82, 104, 52, 16, 16, 249, 132, 105, 22, 239, 201, 80, 12, 173, 86, 52, 168, 79, 77, 54, 49, 94, 151, 60, 33, 41, 6, 151, 190, 154, 84, 253, 218, 216, 119, 82, 1, 26, 157, 156, 48, 32, 162, 54, 91, 22, 141 } });

            migrationBuilder.CreateIndex(
                name: "IX_user_Email",
                table: "user",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user");
        }
    }
}

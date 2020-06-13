using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MMM.Library.Infra.Data.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Books_AuthorId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Books");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "Code", "IsDeleted" },
                values: new object[,]
                {
                    { new Guid("c3348acd-336c-41f8-a43a-9f9008f37dc1"), "Category 01", 1001, false },
                    { new Guid("1b81deeb-e91d-4387-b79a-214d5953e7dc"), "Category 02", 1001, false },
                    { new Guid("ec62f941-0a0e-4c86-a0a6-d6ac02aed73e"), "Category 03", 1001, false },
                    { new Guid("0e92803b-153c-4ea3-b902-317b85695cf5"), "Category 04", 1001, false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("0e92803b-153c-4ea3-b902-317b85695cf5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1b81deeb-e91d-4387-b79a-214d5953e7dc"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("c3348acd-336c-41f8-a43a-9f9008f37dc1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ec62f941-0a0e-4c86-a0a6-d6ac02aed73e"));

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BirthDate = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(250)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastName = table.Column<string>(type: "varchar(250)", nullable: false),
                    Nacionality = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

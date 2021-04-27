using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace teste.dotNet.API.Data.Migrations
{
    public partial class ModifiedBookClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReleaseDate",
                table: "Books",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReleaseDate",
                table: "Books");
        }
    }
}

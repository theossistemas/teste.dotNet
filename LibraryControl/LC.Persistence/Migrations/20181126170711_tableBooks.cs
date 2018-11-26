using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LC.Persistence.Migrations
{
    public partial class tableBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_BOOKS",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NAME = table.Column<string>(nullable: false),
                    DESCRIPTION_SHORT = table.Column<string>(nullable: true),
                    DESCRIPTION_LONG = table.Column<string>(nullable: true),
                    PHOTO = table.Column<string>(nullable: false),
                    CREATED_AT = table.Column<DateTime>(nullable: false),
                    PRICE = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    AUTHOR = table.Column<string>(nullable: false),
                    YEAR = table.Column<int>(nullable: false),
                    LANGUAGE = table.Column<string>(nullable: true),
                    PUBLISHING = table.Column<string>(nullable: false),
                    WEIGHT = table.Column<string>(nullable: true),
                    QUANTITY_PAGES = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_BOOKS", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_BOOKS");
        }
    }
}

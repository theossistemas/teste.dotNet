using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheosLivraria.INFRA.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColumnException : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exception",
                schema: "dbo",
                table: "AuditLogs");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 9, 1, 16, 3, 25, 331, DateTimeKind.Local).AddTicks(6971), new DateTime(2025, 9, 1, 16, 3, 25, 331, DateTimeKind.Local).AddTicks(6985) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 9, 1, 16, 3, 25, 331, DateTimeKind.Local).AddTicks(6988), new DateTime(2025, 9, 1, 16, 3, 25, 331, DateTimeKind.Local).AddTicks(6989) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 9, 1, 16, 3, 25, 331, DateTimeKind.Local).AddTicks(7296), new DateTime(2025, 9, 1, 16, 3, 25, 331, DateTimeKind.Local).AddTicks(7297) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 9, 1, 16, 3, 25, 331, DateTimeKind.Local).AddTicks(7299), new DateTime(2025, 9, 1, 16, 3, 25, 331, DateTimeKind.Local).AddTicks(7300) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Status",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 9, 1, 16, 3, 25, 331, DateTimeKind.Local).AddTicks(7302), new DateTime(2025, 9, 1, 16, 3, 25, 331, DateTimeKind.Local).AddTicks(7303) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Exception",
                schema: "dbo",
                table: "AuditLogs",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 9, 1, 15, 50, 56, 476, DateTimeKind.Local).AddTicks(4527), new DateTime(2025, 9, 1, 15, 50, 56, 476, DateTimeKind.Local).AddTicks(4544) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 9, 1, 15, 50, 56, 476, DateTimeKind.Local).AddTicks(4546), new DateTime(2025, 9, 1, 15, 50, 56, 476, DateTimeKind.Local).AddTicks(4546) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 9, 1, 15, 50, 56, 476, DateTimeKind.Local).AddTicks(5568), new DateTime(2025, 9, 1, 15, 50, 56, 476, DateTimeKind.Local).AddTicks(5572) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 9, 1, 15, 50, 56, 476, DateTimeKind.Local).AddTicks(5575), new DateTime(2025, 9, 1, 15, 50, 56, 476, DateTimeKind.Local).AddTicks(5575) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Status",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 9, 1, 15, 50, 56, 476, DateTimeKind.Local).AddTicks(5577), new DateTime(2025, 9, 1, 15, 50, 56, 476, DateTimeKind.Local).AddTicks(5578) });
        }
    }
}

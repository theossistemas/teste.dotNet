using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheosLivraria.INFRA.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAuditLogDataColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Data",
                schema: "dbo",
                table: "AuditLogs",
                type: "varchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 9, 1, 16, 29, 56, 302, DateTimeKind.Local).AddTicks(2254), new DateTime(2025, 9, 1, 16, 29, 56, 302, DateTimeKind.Local).AddTicks(2267) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 9, 1, 16, 29, 56, 302, DateTimeKind.Local).AddTicks(2269), new DateTime(2025, 9, 1, 16, 29, 56, 302, DateTimeKind.Local).AddTicks(2270) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Status",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 9, 1, 16, 29, 56, 302, DateTimeKind.Local).AddTicks(2488), new DateTime(2025, 9, 1, 16, 29, 56, 302, DateTimeKind.Local).AddTicks(2489) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Status",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 9, 1, 16, 29, 56, 302, DateTimeKind.Local).AddTicks(2490), new DateTime(2025, 9, 1, 16, 29, 56, 302, DateTimeKind.Local).AddTicks(2491) });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Status",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2025, 9, 1, 16, 29, 56, 302, DateTimeKind.Local).AddTicks(2493), new DateTime(2025, 9, 1, 16, 29, 56, 302, DateTimeKind.Local).AddTicks(2493) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Data",
                schema: "dbo",
                table: "AuditLogs",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldNullable: true);

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
    }
}

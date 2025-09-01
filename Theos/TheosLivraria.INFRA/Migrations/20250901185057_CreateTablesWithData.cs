using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheosLivraria.INFRA.Migrations
{
    /// <inheritdoc />
    public partial class CreateTablesWithData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "Status",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "Roles",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Data",
                schema: "dbo",
                table: "AuditLogs",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100);

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Roles",
                columns: new[] { "Id", "Active", "CreatedDate", "Description", "ModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 9, 1, 15, 50, 56, 476, DateTimeKind.Local).AddTicks(4527), null, new DateTime(2025, 9, 1, 15, 50, 56, 476, DateTimeKind.Local).AddTicks(4544), "Admin" },
                    { 2, true, new DateTime(2025, 9, 1, 15, 50, 56, 476, DateTimeKind.Local).AddTicks(4546), null, new DateTime(2025, 9, 1, 15, 50, 56, 476, DateTimeKind.Local).AddTicks(4546), "Public" }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Status",
                columns: new[] { "Id", "Active", "CreatedDate", "Description", "ModifiedDate", "Name", "Type" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2025, 9, 1, 15, 50, 56, 476, DateTimeKind.Local).AddTicks(5568), null, new DateTime(2025, 9, 1, 15, 50, 56, 476, DateTimeKind.Local).AddTicks(5572), "Disponivel", 1 },
                    { 2, true, new DateTime(2025, 9, 1, 15, 50, 56, 476, DateTimeKind.Local).AddTicks(5575), null, new DateTime(2025, 9, 1, 15, 50, 56, 476, DateTimeKind.Local).AddTicks(5575), "Emprestado", 1 },
                    { 3, true, new DateTime(2025, 9, 1, 15, 50, 56, 476, DateTimeKind.Local).AddTicks(5577), null, new DateTime(2025, 9, 1, 15, 50, 56, 476, DateTimeKind.Local).AddTicks(5578), "Reservado", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Status",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Status",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Status",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "Status",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "Roles",
                type: "varchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Data",
                schema: "dbo",
                table: "AuditLogs",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}

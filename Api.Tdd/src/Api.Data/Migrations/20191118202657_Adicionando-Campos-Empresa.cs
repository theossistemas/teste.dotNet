using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AdicionandoCamposEmpresa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Endereco",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RazaoSocial",
                table: "Empresa",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InscricaoMunicipal",
                table: "Empresa",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "InscricaoEstadual",
                table: "Empresa",
                maxLength: 60,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Fantasia",
                table: "Empresa",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "Empresa",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInicio",
                table: "Empresa",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Empresa",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NFCe",
                table: "Empresa",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NFSe",
                table: "Empresa",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "NFe",
                table: "Empresa",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TipoEstoque",
                table: "Empresa",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TipoRegimeTributario",
                table: "Empresa",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "DataInicio",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "NFCe",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "NFSe",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "NFe",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "TipoEstoque",
                table: "Empresa");

            migrationBuilder.DropColumn(
                name: "TipoRegimeTributario",
                table: "Empresa");

            migrationBuilder.AlterColumn<string>(
                name: "RazaoSocial",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "InscricaoMunicipal",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "InscricaoEstadual",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 60);

            migrationBuilder.AlterColumn<string>(
                name: "Fantasia",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Cnpj",
                table: "Empresa",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 30);
        }
    }
}

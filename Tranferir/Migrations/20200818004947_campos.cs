using Microsoft.EntityFrameworkCore.Migrations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tranferir.Migrations
{
    public partial class campos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "CUENTAS",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cedula",
                table: "CUENTAS",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Digitos",
                table: "CUENTAS",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "CUENTAS");

            migrationBuilder.DropColumn(
                name: "Cedula",
                table: "CUENTAS");

            migrationBuilder.DropColumn(
                name: "Digitos",
                table: "CUENTAS");
        }
    }
}

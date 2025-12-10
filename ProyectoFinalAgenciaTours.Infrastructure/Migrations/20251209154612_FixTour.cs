using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinalAgenciaTours.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixTour : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HoraInicio",
                table: "Tours",
                newName: "Hora");

            migrationBuilder.RenameColumn(
                name: "FechaInicio",
                table: "Tours",
                newName: "FechaFinalizacion");

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "Tours",
                type: "decimal(9,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<string>(
                name: "Duracion",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Tours",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "Tours",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "TasaImpuesto",
                table: "Tours",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duracion",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "TasaImpuesto",
                table: "Tours");

            migrationBuilder.RenameColumn(
                name: "Hora",
                table: "Tours",
                newName: "HoraInicio");

            migrationBuilder.RenameColumn(
                name: "FechaFinalizacion",
                table: "Tours",
                newName: "FechaInicio");

            migrationBuilder.AlterColumn<decimal>(
                name: "Precio",
                table: "Tours",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,2)");
        }
    }
}

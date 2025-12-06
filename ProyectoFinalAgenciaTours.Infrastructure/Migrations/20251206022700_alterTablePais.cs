using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinalAgenciaTours.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class alterTablePais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoISO",
                table: "Paises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoISO",
                table: "Paises");
        }
    }
}

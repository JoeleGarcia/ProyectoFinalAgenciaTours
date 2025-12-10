using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinalAgenciaTours.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddITBIS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ITBIS",
                table: "Tours",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ITBIS",
                table: "Tours");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MonsterGym.Migrations
{
    /// <inheritdoc />
    public partial class agregarclavecleinteempelado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Contrasena",
                table: "Empleados",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Contrasena",
                table: "Clientes",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Contrasena",
                table: "Empleados");

            migrationBuilder.DropColumn(
                name: "Contrasena",
                table: "Clientes");
        }
    }
}

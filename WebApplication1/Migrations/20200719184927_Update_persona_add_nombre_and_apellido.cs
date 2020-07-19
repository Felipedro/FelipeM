using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication1.Migrations
{
    public partial class Update_persona_add_nombre_and_apellido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "apellido",
                table: "Persona",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "nombre",
                table: "Persona",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "apellido",
                table: "Persona");

            migrationBuilder.DropColumn(
                name: "nombre",
                table: "Persona");
        }
    }
}

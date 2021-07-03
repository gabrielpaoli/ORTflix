using Microsoft.EntityFrameworkCore.Migrations;

namespace ORTflix.Migrations
{
    public partial class ORTflixContextPeliculaDatabaseContextAgregamosVotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VotoNegativo",
                table: "Peliculas",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "VotoPositivo",
                table: "Peliculas",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VotoNegativo",
                table: "Peliculas");

            migrationBuilder.DropColumn(
                name: "VotoPositivo",
                table: "Peliculas");
        }
    }
}

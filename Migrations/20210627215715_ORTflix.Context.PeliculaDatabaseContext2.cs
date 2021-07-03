using Microsoft.EntityFrameworkCore.Migrations;

namespace ORTflix.Migrations
{
    public partial class ORTflixContextPeliculaDatabaseContext2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imagen",
                table: "Peliculas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagen",
                table: "Peliculas");
        }
    }
}

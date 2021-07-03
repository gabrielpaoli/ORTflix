using Microsoft.EntityFrameworkCore.Migrations;

namespace ORTflix.Migrations
{
    public partial class ORTflixContextPeliculaDatabaseContext7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Peliculas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Peliculas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Peliculas");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "Peliculas");
        }
    }
}

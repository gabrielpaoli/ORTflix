using Microsoft.EntityFrameworkCore.Migrations;

namespace ORTflix.Migrations
{
    public partial class ORTflixContextPeliculaDatabaseContext6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TipoDeVoto",
                table: "VotosDelUsuario",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TipoDeVoto",
                table: "VotosDelUsuario");
        }
    }
}

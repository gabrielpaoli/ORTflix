using Microsoft.EntityFrameworkCore.Migrations;

namespace ORTflix.Migrations
{
    public partial class ORTflixContextPeliculaDatabaseContext11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "role",
                table: "Usuarios",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "role",
                table: "Usuarios");
        }
    }
}

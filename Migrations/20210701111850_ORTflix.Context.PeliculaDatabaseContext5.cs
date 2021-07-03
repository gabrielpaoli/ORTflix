using Microsoft.EntityFrameworkCore.Migrations;

namespace ORTflix.Migrations
{
    public partial class ORTflixContextPeliculaDatabaseContext5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VotosDelUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(nullable: true),
                    PeliculaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotosDelUsuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VotosDelUsuario_Peliculas_PeliculaId",
                        column: x => x.PeliculaId,
                        principalTable: "Peliculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VotosDelUsuario_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VotosDelUsuario_PeliculaId",
                table: "VotosDelUsuario",
                column: "PeliculaId");

            migrationBuilder.CreateIndex(
                name: "IX_VotosDelUsuario_UsuarioId",
                table: "VotosDelUsuario",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VotosDelUsuario");
        }
    }
}

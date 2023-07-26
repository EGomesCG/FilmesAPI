using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmesApi.Migrations
{
    public partial class SessaoeFilme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Criando tabela Sessoes
            migrationBuilder.CreateTable(
                name: "Sessoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)//A tabela contém o campo ID e FilmeId
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FilmeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessoes", x => x.Id);//Chave primaria
                    table.ForeignKey(
                        name: "FK_Sessoes_Filmes_FilmeId",
                        column: x => x.FilmeId,//Chave estrangeira
                        principalTable: "Filmes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Sessoes_FilmeId",
                table: "Sessoes",
                column: "FilmeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sessoes");
        }
    }
}

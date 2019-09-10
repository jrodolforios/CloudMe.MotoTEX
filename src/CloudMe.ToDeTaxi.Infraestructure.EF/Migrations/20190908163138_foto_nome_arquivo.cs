using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Migrations
{
    public partial class foto_nome_arquivo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Foto",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeArquivo",
                table: "Foto",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Foto");

            migrationBuilder.DropColumn(
                name: "NomeArquivo",
                table: "Foto");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Migrations
{
    public partial class ponto_taxi_endereco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PontoTaxi_Localizacao_IdEndereco",
                table: "PontoTaxi");

            migrationBuilder.AddForeignKey(
                name: "FK_PontoTaxi_Endereco_IdEndereco",
                table: "PontoTaxi",
                column: "IdEndereco",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PontoTaxi_Endereco_IdEndereco",
                table: "PontoTaxi");

            migrationBuilder.AddForeignKey(
                name: "FK_PontoTaxi_Localizacao_IdEndereco",
                table: "PontoTaxi",
                column: "IdEndereco",
                principalTable: "Localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

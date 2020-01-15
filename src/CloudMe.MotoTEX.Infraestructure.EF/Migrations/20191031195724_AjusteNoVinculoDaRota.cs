using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.MotoTEX.Infraestructure.EF.Migrations
{
    public partial class AjusteNoVinculoDaRota : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Corrida_Rota_IdVeiculo",
                table: "Corrida");

            migrationBuilder.CreateIndex(
                name: "IX_Corrida_IdRotaExecutada",
                table: "Corrida",
                column: "IdRotaExecutada");

            migrationBuilder.AddForeignKey(
                name: "FK_Corrida_Rota_IdRotaExecutada",
                table: "Corrida",
                column: "IdRotaExecutada",
                principalTable: "Rota",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Corrida_Rota_IdRotaExecutada",
                table: "Corrida");

            migrationBuilder.DropIndex(
                name: "IX_Corrida_IdRotaExecutada",
                table: "Corrida");

            migrationBuilder.AddForeignKey(
                name: "FK_Corrida_Rota_IdVeiculo",
                table: "Corrida",
                column: "IdVeiculo",
                principalTable: "Rota",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

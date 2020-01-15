using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.MotoTEX.Infraestructure.EF.Migrations
{
    public partial class RotaNaoObrigatoriaCorrida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Corrida_Rota_IdRotaExecutada",
                table: "Corrida");

            migrationBuilder.DropIndex(
                name: "IX_Favorito_IdTaxista",
                table: "Favorito");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdRotaExecutada",
                table: "Corrida",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_Favorito_IdTaxista",
                table: "Favorito",
                column: "IdTaxista");

            migrationBuilder.AddForeignKey(
                name: "FK_Corrida_Rota_IdRotaExecutada",
                table: "Corrida",
                column: "IdRotaExecutada",
                principalTable: "Rota",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Corrida_Rota_IdRotaExecutada",
                table: "Corrida");

            migrationBuilder.DropIndex(
                name: "IX_Favorito_IdTaxista",
                table: "Favorito");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdRotaExecutada",
                table: "Corrida",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favorito_IdTaxista",
                table: "Favorito",
                column: "IdTaxista",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Corrida_Rota_IdRotaExecutada",
                table: "Corrida",
                column: "IdRotaExecutada",
                principalTable: "Rota",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

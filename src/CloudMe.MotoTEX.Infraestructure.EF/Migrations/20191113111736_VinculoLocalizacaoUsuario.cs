using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.MotoTEX.Infraestructure.EF.Migrations
{
    public partial class VinculoLocalizacaoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdUsuario",
                table: "Localizacao",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Localizacao_IdUsuario",
                table: "Localizacao",
                column: "IdUsuario",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Localizacao_Users_IdUsuario",
                table: "Localizacao",
                column: "IdUsuario",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Localizacao_Users_IdUsuario",
                table: "Localizacao");

            migrationBuilder.DropIndex(
                name: "IX_Localizacao_IdUsuario",
                table: "Localizacao");

            migrationBuilder.DropColumn(
                name: "IdUsuario",
                table: "Localizacao");
        }
    }
}

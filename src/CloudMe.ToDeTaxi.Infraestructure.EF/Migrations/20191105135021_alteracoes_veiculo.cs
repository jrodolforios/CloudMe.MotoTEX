using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Migrations
{
    public partial class alteracoes_veiculo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ano",
                table: "Veiculo",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "IdCorVeiculo",
                table: "Veiculo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Versao",
                table: "Veiculo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_IdCorVeiculo",
                table: "Veiculo",
                column: "IdCorVeiculo",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculo_CorVeiculo_IdCorVeiculo",
                table: "Veiculo",
                column: "IdCorVeiculo",
                principalTable: "CorVeiculo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Veiculo_CorVeiculo_IdCorVeiculo",
                table: "Veiculo");

            migrationBuilder.DropIndex(
                name: "IX_Veiculo_IdCorVeiculo",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "Ano",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "IdCorVeiculo",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "Versao",
                table: "Veiculo");
        }
    }
}

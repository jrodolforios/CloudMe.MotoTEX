using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.MotoTEX.Infraestructure.EF.Migrations
{
    public partial class FaixaDescontoOpcional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoCorrida_FaixaDesconto_IdFaixaDesconto",
                table: "SolicitacaoCorrida");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdFaixaDesconto",
                table: "SolicitacaoCorrida",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoCorrida_FaixaDesconto_IdFaixaDesconto",
                table: "SolicitacaoCorrida",
                column: "IdFaixaDesconto",
                principalTable: "FaixaDesconto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoCorrida_FaixaDesconto_IdFaixaDesconto",
                table: "SolicitacaoCorrida");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdFaixaDesconto",
                table: "SolicitacaoCorrida",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoCorrida_FaixaDesconto_IdFaixaDesconto",
                table: "SolicitacaoCorrida",
                column: "IdFaixaDesconto",
                principalTable: "FaixaDesconto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

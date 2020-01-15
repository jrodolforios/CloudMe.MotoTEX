﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.MotoTEX.Infraestructure.EF.Migrations
{
    public partial class RotaNaoObrigatoriaSolicitacaoCorrida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoCorrida_Rota_IdRota",
                table: "SolicitacaoCorrida");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdRota",
                table: "SolicitacaoCorrida",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoCorrida_Rota_IdRota",
                table: "SolicitacaoCorrida",
                column: "IdRota",
                principalTable: "Rota",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoCorrida_Rota_IdRota",
                table: "SolicitacaoCorrida");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdRota",
                table: "SolicitacaoCorrida",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoCorrida_Rota_IdRota",
                table: "SolicitacaoCorrida",
                column: "IdRota",
                principalTable: "Rota",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.MotoTEX.Infraestructure.EF.Migrations
{
    public partial class ContatoAjusteNullabeTaxistaEPassageiro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contato_Passageiro_IdPassageiro",
                table: "Contato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contato_Taxista_IdTaxista",
                table: "Contato");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdTaxista",
                table: "Contato",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "IdPassageiro",
                table: "Contato",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Contato_Passageiro_IdPassageiro",
                table: "Contato",
                column: "IdPassageiro",
                principalTable: "Passageiro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contato_Taxista_IdTaxista",
                table: "Contato",
                column: "IdTaxista",
                principalTable: "Taxista",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contato_Passageiro_IdPassageiro",
                table: "Contato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contato_Taxista_IdTaxista",
                table: "Contato");

            migrationBuilder.AlterColumn<Guid>(
                name: "IdTaxista",
                table: "Contato",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "IdPassageiro",
                table: "Contato",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contato_Passageiro_IdPassageiro",
                table: "Contato",
                column: "IdPassageiro",
                principalTable: "Passageiro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contato_Taxista_IdTaxista",
                table: "Contato",
                column: "IdTaxista",
                principalTable: "Taxista",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

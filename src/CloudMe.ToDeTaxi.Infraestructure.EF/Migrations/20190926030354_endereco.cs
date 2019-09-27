using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Migrations
{
    public partial class endereco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Taxista_Localizacao_IdEndereco",
                table: "Taxista");

            migrationBuilder.CreateTable(
                name: "Endereco",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CEP = table.Column<string>(nullable: false),
                    Logradouro = table.Column<string>(nullable: false),
                    Numero = table.Column<string>(nullable: false),
                    Complemento = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: false),
                    Localidade = table.Column<string>(nullable: false),
                    UF = table.Column<string>(nullable: false),
                    IdLocalizacao = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Endereco", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Endereco_Localizacao_IdLocalizacao",
                        column: x => x.IdLocalizacao,
                        principalTable: "Localizacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_IdLocalizacao",
                table: "Endereco",
                column: "IdLocalizacao",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Taxista_Endereco_IdEndereco",
                table: "Taxista",
                column: "IdEndereco",
                principalTable: "Endereco",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Taxista_Endereco_IdEndereco",
                table: "Taxista");

            migrationBuilder.DropTable(
                name: "Endereco");

            migrationBuilder.AddForeignKey(
                name: "FK_Taxista_Localizacao_IdEndereco",
                table: "Taxista",
                column: "IdEndereco",
                principalTable: "Localizacao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

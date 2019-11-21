using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Migrations
{
    public partial class MonitoramentoSolicitacaoCorrida : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusMonitoramento",
                table: "SolicitacaoCorrida",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SolicitacaoCorridaTaxista",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    IdSolicitacaoCorrida = table.Column<Guid>(nullable: false),
                    IdTaxista = table.Column<Guid>(nullable: false),
                    Acao = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacaoCorridaTaxista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitacaoCorridaTaxista_SolicitacaoCorrida_IdSolicitacaoC~",
                        column: x => x.IdSolicitacaoCorrida,
                        principalTable: "SolicitacaoCorrida",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitacaoCorridaTaxista_Taxista_IdTaxista",
                        column: x => x.IdTaxista,
                        principalTable: "Taxista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorridaTaxista_IdSolicitacaoCorrida",
                table: "SolicitacaoCorridaTaxista",
                column: "IdSolicitacaoCorrida");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorridaTaxista_IdTaxista",
                table: "SolicitacaoCorridaTaxista",
                column: "IdTaxista");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitacaoCorridaTaxista");

            migrationBuilder.DropColumn(
                name: "StatusMonitoramento",
                table: "SolicitacaoCorrida");
        }
    }
}

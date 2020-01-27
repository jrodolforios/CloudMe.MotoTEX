using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.MotoTEX.Infraestructure.EF.Migrations
{
    public partial class FaturamentoModulo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "IdFaturamentoTaxista",
                table: "Corrida",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Faturamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    Ano = table.Column<int>(nullable: false),
                    Mes = table.Column<int>(nullable: false),
                    Total = table.Column<decimal>(nullable: true),
                    PercentualComissao = table.Column<decimal>(nullable: false),
                    DataGeracao = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faturamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Faturamento_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Faturamento_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Faturamento_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FaturamentoTaxista",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    InsertUserId = table.Column<Guid>(nullable: true),
                    UpdateUserId = table.Column<Guid>(nullable: true),
                    DeleteUserId = table.Column<Guid>(nullable: true),
                    IdFaturamento = table.Column<Guid>(nullable: false),
                    FaturamentoId = table.Column<Guid>(nullable: true),
                    IdTaxista = table.Column<Guid>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    LinkBoleto = table.Column<string>(nullable: true),
                    JsonBoletoAPI = table.Column<string>(nullable: true),
                    DataGeracao = table.Column<DateTime>(nullable: false),
                    DataVencimento = table.Column<DateTime>(nullable: false),
                    DataPagamento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaturamentoTaxista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaturamentoTaxista_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FaturamentoTaxista_Faturamento_FaturamentoId",
                        column: x => x.FaturamentoId,
                        principalTable: "Faturamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FaturamentoTaxista_Taxista_IdTaxista",
                        column: x => x.IdTaxista,
                        principalTable: "Taxista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaturamentoTaxista_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FaturamentoTaxista_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Corrida_IdFaturamentoTaxista",
                table: "Corrida",
                column: "IdFaturamentoTaxista");

            migrationBuilder.CreateIndex(
                name: "IX_Faturamento_DeleteUserId",
                table: "Faturamento",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Faturamento_InsertUserId",
                table: "Faturamento",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Faturamento_UpdateUserId",
                table: "Faturamento",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoTaxista_DeleteUserId",
                table: "FaturamentoTaxista",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoTaxista_FaturamentoId",
                table: "FaturamentoTaxista",
                column: "FaturamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoTaxista_IdTaxista",
                table: "FaturamentoTaxista",
                column: "IdTaxista");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoTaxista_InsertUserId",
                table: "FaturamentoTaxista",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoTaxista_UpdateUserId",
                table: "FaturamentoTaxista",
                column: "UpdateUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Corrida_FaturamentoTaxista_IdFaturamentoTaxista",
                table: "Corrida",
                column: "IdFaturamentoTaxista",
                principalTable: "FaturamentoTaxista",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Corrida_FaturamentoTaxista_IdFaturamentoTaxista",
                table: "Corrida");

            migrationBuilder.DropTable(
                name: "FaturamentoTaxista");

            migrationBuilder.DropTable(
                name: "Faturamento");

            migrationBuilder.DropIndex(
                name: "IX_Corrida_IdFaturamentoTaxista",
                table: "Corrida");

            migrationBuilder.DropColumn(
                name: "IdFaturamentoTaxista",
                table: "Corrida");
        }
    }
}

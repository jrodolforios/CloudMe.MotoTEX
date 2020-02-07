using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.MotoTEX.Infraestructure.EF.Migrations
{
    public partial class faixas_ativacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaturamentoTaxista_Faturamento_FaturamentoId",
                table: "FaturamentoTaxista");

            migrationBuilder.DropIndex(
                name: "IX_FaturamentoTaxista_FaturamentoId",
                table: "FaturamentoTaxista");

            migrationBuilder.DropColumn(
                name: "FaturamentoId",
                table: "FaturamentoTaxista");

            migrationBuilder.CreateTable(
                name: "FaixaAtivacao",
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
                    Raio = table.Column<double>(nullable: false, defaultValue: 0.0),
                    Janela = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaixaAtivacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaixaAtivacao_Users_DeleteUserId",
                        column: x => x.DeleteUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FaixaAtivacao_Users_InsertUserId",
                        column: x => x.InsertUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FaixaAtivacao_Users_UpdateUserId",
                        column: x => x.UpdateUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoTaxista_IdFaturamento",
                table: "FaturamentoTaxista",
                column: "IdFaturamento");

            migrationBuilder.CreateIndex(
                name: "IX_FaixaAtivacao_DeleteUserId",
                table: "FaixaAtivacao",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaixaAtivacao_InsertUserId",
                table: "FaixaAtivacao",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaixaAtivacao_UpdateUserId",
                table: "FaixaAtivacao",
                column: "UpdateUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FaturamentoTaxista_Faturamento_IdFaturamento",
                table: "FaturamentoTaxista",
                column: "IdFaturamento",
                principalTable: "Faturamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FaturamentoTaxista_Faturamento_IdFaturamento",
                table: "FaturamentoTaxista");

            migrationBuilder.DropTable(
                name: "FaixaAtivacao");

            migrationBuilder.DropIndex(
                name: "IX_FaturamentoTaxista_IdFaturamento",
                table: "FaturamentoTaxista");

            migrationBuilder.AddColumn<Guid>(
                name: "FaturamentoId",
                table: "FaturamentoTaxista",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoTaxista_FaturamentoId",
                table: "FaturamentoTaxista",
                column: "FaturamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_FaturamentoTaxista_Faturamento_FaturamentoId",
                table: "FaturamentoTaxista",
                column: "FaturamentoId",
                principalTable: "Faturamento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

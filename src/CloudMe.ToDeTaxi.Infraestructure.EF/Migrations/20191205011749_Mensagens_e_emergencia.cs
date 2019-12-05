using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Migrations
{
    public partial class Mensagens_e_emergencia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Emergencias",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    IdTaxista = table.Column<Guid>(nullable: false),
                    TaxistaId = table.Column<Guid>(nullable: true),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emergencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Emergencias_Taxista_TaxistaId",
                        column: x => x.TaxistaId,
                        principalTable: "Taxista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Mensagem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    IdRemetente = table.Column<Guid>(nullable: false),
                    Assunto = table.Column<string>(nullable: true),
                    Corpo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensagem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mensagem_Users_IdRemetente",
                        column: x => x.IdRemetente,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MensagemDestinatario",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    IdMensagem = table.Column<Guid>(nullable: false),
                    IdUsuario = table.Column<Guid>(nullable: false),
                    IdGrupoUsuario = table.Column<Guid>(nullable: true),
                    DataRecebimento = table.Column<DateTime>(nullable: true),
                    DataLeitura = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MensagemDestinatario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MensagemDestinatario_GrupoUsuario_IdGrupoUsuario",
                        column: x => x.IdGrupoUsuario,
                        principalTable: "GrupoUsuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MensagemDestinatario_Mensagem_IdMensagem",
                        column: x => x.IdMensagem,
                        principalTable: "Mensagem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MensagemDestinatario_Users_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emergencias_TaxistaId",
                table: "Emergencias",
                column: "TaxistaId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_IdRemetente",
                table: "Mensagem",
                column: "IdRemetente");

            migrationBuilder.CreateIndex(
                name: "IX_MensagemDestinatario_IdGrupoUsuario",
                table: "MensagemDestinatario",
                column: "IdGrupoUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_MensagemDestinatario_IdMensagem",
                table: "MensagemDestinatario",
                column: "IdMensagem");

            migrationBuilder.CreateIndex(
                name: "IX_MensagemDestinatario_IdUsuario",
                table: "MensagemDestinatario",
                column: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emergencias");

            migrationBuilder.DropTable(
                name: "MensagemDestinatario");

            migrationBuilder.DropTable(
                name: "Mensagem");
        }
    }
}

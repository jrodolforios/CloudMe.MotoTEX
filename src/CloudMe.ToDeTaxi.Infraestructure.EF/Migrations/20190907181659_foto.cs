using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Migrations
{
    public partial class foto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Taxista");

            migrationBuilder.DropColumn(
                name: "Foto",
                table: "Passageiro");

            migrationBuilder.AddColumn<Guid>(
                name: "IdFoto",
                table: "Veiculo",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdFoto",
                table: "Taxista",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdFoto",
                table: "Passageiro",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Foto",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Dados = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foto", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_IdFoto",
                table: "Veiculo",
                column: "IdFoto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Taxista_IdFoto",
                table: "Taxista",
                column: "IdFoto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passageiro_IdFoto",
                table: "Passageiro",
                column: "IdFoto",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Passageiro_Foto_IdFoto",
                table: "Passageiro",
                column: "IdFoto",
                principalTable: "Foto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Taxista_Foto_IdFoto",
                table: "Taxista",
                column: "IdFoto",
                principalTable: "Foto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculo_Foto_IdFoto",
                table: "Veiculo",
                column: "IdFoto",
                principalTable: "Foto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Passageiro_Foto_IdFoto",
                table: "Passageiro");

            migrationBuilder.DropForeignKey(
                name: "FK_Taxista_Foto_IdFoto",
                table: "Taxista");

            migrationBuilder.DropForeignKey(
                name: "FK_Veiculo_Foto_IdFoto",
                table: "Veiculo");

            migrationBuilder.DropTable(
                name: "Foto");

            migrationBuilder.DropIndex(
                name: "IX_Veiculo_IdFoto",
                table: "Veiculo");

            migrationBuilder.DropIndex(
                name: "IX_Taxista_IdFoto",
                table: "Taxista");

            migrationBuilder.DropIndex(
                name: "IX_Passageiro_IdFoto",
                table: "Passageiro");

            migrationBuilder.DropColumn(
                name: "IdFoto",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "IdFoto",
                table: "Taxista");

            migrationBuilder.DropColumn(
                name: "IdFoto",
                table: "Passageiro");

            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "Veiculo",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "Taxista",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "Passageiro",
                nullable: false,
                defaultValue: new byte[] {  });
        }
    }
}

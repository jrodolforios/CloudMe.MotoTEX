using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Migrations
{
    public partial class Contratos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contrato",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ForceDelete = table.Column<bool>(nullable: false),
                    Inserted = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Deleted = table.Column<DateTime>(nullable: true),
                    Conteudo = table.Column<string>(nullable: false),
                    UltimaVersao = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contrato", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contrato");
        }
    }
}

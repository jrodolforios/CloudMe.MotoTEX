using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.MotoTEX.Infraestructure.EF.Migrations
{
    public partial class angulo_localizacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Angulo",
                table: "Localizacao",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Angulo",
                table: "Localizacao");
        }
    }
}

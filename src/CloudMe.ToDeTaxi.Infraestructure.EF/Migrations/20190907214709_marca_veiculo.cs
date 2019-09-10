using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Migrations
{
    public partial class marca_veiculo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Cor",
                table: "Veiculo",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Marca",
                table: "Veiculo",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Marca",
                table: "Veiculo");

            migrationBuilder.AlterColumn<int>(
                name: "Cor",
                table: "Veiculo",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}

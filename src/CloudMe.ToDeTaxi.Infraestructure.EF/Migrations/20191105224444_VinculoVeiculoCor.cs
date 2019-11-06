using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Migrations
{
    public partial class VinculoVeiculoCor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Veiculo_IdCorVeiculo",
                table: "Veiculo");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_IdCorVeiculo",
                table: "Veiculo",
                column: "IdCorVeiculo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Veiculo_IdCorVeiculo",
                table: "Veiculo");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_IdCorVeiculo",
                table: "Veiculo",
                column: "IdCorVeiculo",
                unique: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.MotoTEX.Infraestructure.EF.Migrations
{
    public partial class TriggersParaSoftDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "VeiculoTaxista");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UsuarioGrupoUsuario");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Taxista");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tarifa");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "SolicitacaoCorrida");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Rota");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PontoTaxi");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Passageiro");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Localizacao");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "GrupoUsuario");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Foto");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FormaPagamentoTaxista");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FormaPagamento");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Favorito");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FaixaDescontoTaxista");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FaixaDesconto");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CorVeiculo");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Corrida");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "VeiculoTaxista",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Veiculo",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UsuarioGrupoUsuario",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Taxista",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tarifa",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "SolicitacaoCorrida",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Rota",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PontoTaxi",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Passageiro",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Localizacao",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "GrupoUsuario",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Foto",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FormaPagamentoTaxista",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FormaPagamento",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Favorito",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FaixaDescontoTaxista",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FaixaDesconto",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Endereco",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CorVeiculo",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Corrida",
                nullable: false,
                defaultValue: false);
        }
    }
}

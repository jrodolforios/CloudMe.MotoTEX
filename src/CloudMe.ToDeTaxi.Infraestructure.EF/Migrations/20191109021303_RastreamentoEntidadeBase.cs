using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.ToDeTaxi.Infraestructure.EF.Migrations
{
    public partial class RastreamentoEntidadeBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "VeiculoTaxista",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "VeiculoTaxista",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "VeiculoTaxista",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Veiculo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "Veiculo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Veiculo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "UsuarioGrupoUsuario",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "UsuarioGrupoUsuario",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "UsuarioGrupoUsuario",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Taxista",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "Taxista",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Taxista",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Tarifa",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "Tarifa",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Tarifa",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "SolicitacaoCorrida",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "SolicitacaoCorrida",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "SolicitacaoCorrida",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Rota",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "Rota",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Rota",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "PontoTaxi",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "PontoTaxi",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "PontoTaxi",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Passageiro",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "Passageiro",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Passageiro",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Localizacao",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "Localizacao",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Localizacao",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "GrupoUsuario",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "GrupoUsuario",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "GrupoUsuario",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Foto",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "Foto",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Foto",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "FormaPagamentoTaxista",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "FormaPagamentoTaxista",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "FormaPagamentoTaxista",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "FormaPagamento",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "FormaPagamento",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "FormaPagamento",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Favorito",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "Favorito",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Favorito",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "FaixaDescontoTaxista",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "FaixaDescontoTaxista",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "FaixaDescontoTaxista",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "FaixaDesconto",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "FaixaDesconto",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "FaixaDesconto",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Endereco",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "Endereco",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Endereco",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "CorVeiculo",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "CorVeiculo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "CorVeiculo",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Deleted",
                table: "Corrida",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Inserted",
                table: "Corrida",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated",
                table: "Corrida",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "VeiculoTaxista");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "VeiculoTaxista");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "VeiculoTaxista");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "UsuarioGrupoUsuario");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "UsuarioGrupoUsuario");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "UsuarioGrupoUsuario");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Taxista");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "Taxista");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Taxista");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Tarifa");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "Tarifa");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Tarifa");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "SolicitacaoCorrida");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "SolicitacaoCorrida");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "SolicitacaoCorrida");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Rota");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "Rota");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Rota");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "PontoTaxi");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "PontoTaxi");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "PontoTaxi");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Passageiro");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "Passageiro");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Passageiro");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Localizacao");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "Localizacao");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Localizacao");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "GrupoUsuario");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "GrupoUsuario");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "GrupoUsuario");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Foto");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "Foto");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Foto");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "FormaPagamentoTaxista");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "FormaPagamentoTaxista");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "FormaPagamentoTaxista");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "FormaPagamento");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "FormaPagamento");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "FormaPagamento");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Favorito");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "Favorito");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Favorito");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "FaixaDescontoTaxista");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "FaixaDescontoTaxista");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "FaixaDescontoTaxista");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "FaixaDesconto");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "FaixaDesconto");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "FaixaDesconto");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "CorVeiculo");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "CorVeiculo");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "CorVeiculo");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Corrida");

            migrationBuilder.DropColumn(
                name: "Inserted",
                table: "Corrida");

            migrationBuilder.DropColumn(
                name: "Updated",
                table: "Corrida");
        }
    }
}

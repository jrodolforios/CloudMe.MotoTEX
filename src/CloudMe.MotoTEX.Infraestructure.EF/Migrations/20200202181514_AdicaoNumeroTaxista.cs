using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CloudMe.MotoTEX.Infraestructure.EF.Migrations
{
    public partial class AdicaoNumeroTaxista : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contato_Users_DeleteUserId",
                table: "Contato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contato_Users_InsertUserId",
                table: "Contato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contato_Users_UpdateUserId",
                table: "Contato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Users_DeleteUserId",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Users_InsertUserId",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Contrato_Users_UpdateUserId",
                table: "Contrato");

            migrationBuilder.DropForeignKey(
                name: "FK_Corrida_Users_DeleteUserId",
                table: "Corrida");

            migrationBuilder.DropForeignKey(
                name: "FK_Corrida_Users_InsertUserId",
                table: "Corrida");

            migrationBuilder.DropForeignKey(
                name: "FK_Corrida_Users_UpdateUserId",
                table: "Corrida");

            migrationBuilder.DropForeignKey(
                name: "FK_CorVeiculo_Users_DeleteUserId",
                table: "CorVeiculo");

            migrationBuilder.DropForeignKey(
                name: "FK_CorVeiculo_Users_InsertUserId",
                table: "CorVeiculo");

            migrationBuilder.DropForeignKey(
                name: "FK_CorVeiculo_Users_UpdateUserId",
                table: "CorVeiculo");

            migrationBuilder.DropForeignKey(
                name: "FK_Emergencias_Users_DeleteUserId",
                table: "Emergencias");

            migrationBuilder.DropForeignKey(
                name: "FK_Emergencias_Users_InsertUserId",
                table: "Emergencias");

            migrationBuilder.DropForeignKey(
                name: "FK_Emergencias_Users_UpdateUserId",
                table: "Emergencias");

            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Users_DeleteUserId",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Users_InsertUserId",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_Endereco_Users_UpdateUserId",
                table: "Endereco");

            migrationBuilder.DropForeignKey(
                name: "FK_FaixaDesconto_Users_DeleteUserId",
                table: "FaixaDesconto");

            migrationBuilder.DropForeignKey(
                name: "FK_FaixaDesconto_Users_InsertUserId",
                table: "FaixaDesconto");

            migrationBuilder.DropForeignKey(
                name: "FK_FaixaDesconto_Users_UpdateUserId",
                table: "FaixaDesconto");

            migrationBuilder.DropForeignKey(
                name: "FK_FaixaDescontoTaxista_Users_DeleteUserId",
                table: "FaixaDescontoTaxista");

            migrationBuilder.DropForeignKey(
                name: "FK_FaixaDescontoTaxista_Users_InsertUserId",
                table: "FaixaDescontoTaxista");

            migrationBuilder.DropForeignKey(
                name: "FK_FaixaDescontoTaxista_Users_UpdateUserId",
                table: "FaixaDescontoTaxista");

            migrationBuilder.DropForeignKey(
                name: "FK_Faturamento_Users_DeleteUserId",
                table: "Faturamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Faturamento_Users_InsertUserId",
                table: "Faturamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Faturamento_Users_UpdateUserId",
                table: "Faturamento");

            migrationBuilder.DropForeignKey(
                name: "FK_FaturamentoTaxista_Users_DeleteUserId",
                table: "FaturamentoTaxista");

            migrationBuilder.DropForeignKey(
                name: "FK_FaturamentoTaxista_Users_InsertUserId",
                table: "FaturamentoTaxista");

            migrationBuilder.DropForeignKey(
                name: "FK_FaturamentoTaxista_Users_UpdateUserId",
                table: "FaturamentoTaxista");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorito_Users_DeleteUserId",
                table: "Favorito");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorito_Users_InsertUserId",
                table: "Favorito");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorito_Users_UpdateUserId",
                table: "Favorito");

            migrationBuilder.DropForeignKey(
                name: "FK_FormaPagamento_Users_DeleteUserId",
                table: "FormaPagamento");

            migrationBuilder.DropForeignKey(
                name: "FK_FormaPagamento_Users_InsertUserId",
                table: "FormaPagamento");

            migrationBuilder.DropForeignKey(
                name: "FK_FormaPagamento_Users_UpdateUserId",
                table: "FormaPagamento");

            migrationBuilder.DropForeignKey(
                name: "FK_FormaPagamentoTaxista_Users_DeleteUserId",
                table: "FormaPagamentoTaxista");

            migrationBuilder.DropForeignKey(
                name: "FK_FormaPagamentoTaxista_Users_InsertUserId",
                table: "FormaPagamentoTaxista");

            migrationBuilder.DropForeignKey(
                name: "FK_FormaPagamentoTaxista_Users_UpdateUserId",
                table: "FormaPagamentoTaxista");

            migrationBuilder.DropForeignKey(
                name: "FK_Foto_Users_DeleteUserId",
                table: "Foto");

            migrationBuilder.DropForeignKey(
                name: "FK_Foto_Users_InsertUserId",
                table: "Foto");

            migrationBuilder.DropForeignKey(
                name: "FK_Foto_Users_UpdateUserId",
                table: "Foto");

            migrationBuilder.DropForeignKey(
                name: "FK_GrupoUsuario_Users_DeleteUserId",
                table: "GrupoUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_GrupoUsuario_Users_InsertUserId",
                table: "GrupoUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_GrupoUsuario_Users_UpdateUserId",
                table: "GrupoUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Localizacao_Users_DeleteUserId",
                table: "Localizacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Localizacao_Users_InsertUserId",
                table: "Localizacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Localizacao_Users_UpdateUserId",
                table: "Localizacao");

            migrationBuilder.DropForeignKey(
                name: "FK_Mensagem_Users_DeleteUserId",
                table: "Mensagem");

            migrationBuilder.DropForeignKey(
                name: "FK_Mensagem_Users_InsertUserId",
                table: "Mensagem");

            migrationBuilder.DropForeignKey(
                name: "FK_Mensagem_Users_UpdateUserId",
                table: "Mensagem");

            migrationBuilder.DropForeignKey(
                name: "FK_MensagemDestinatario_Users_DeleteUserId",
                table: "MensagemDestinatario");

            migrationBuilder.DropForeignKey(
                name: "FK_MensagemDestinatario_Users_InsertUserId",
                table: "MensagemDestinatario");

            migrationBuilder.DropForeignKey(
                name: "FK_MensagemDestinatario_Users_UpdateUserId",
                table: "MensagemDestinatario");

            migrationBuilder.DropForeignKey(
                name: "FK_Passageiro_Users_DeleteUserId",
                table: "Passageiro");

            migrationBuilder.DropForeignKey(
                name: "FK_Passageiro_Users_InsertUserId",
                table: "Passageiro");

            migrationBuilder.DropForeignKey(
                name: "FK_Passageiro_Users_UpdateUserId",
                table: "Passageiro");

            migrationBuilder.DropForeignKey(
                name: "FK_PontoTaxi_Users_DeleteUserId",
                table: "PontoTaxi");

            migrationBuilder.DropForeignKey(
                name: "FK_PontoTaxi_Users_InsertUserId",
                table: "PontoTaxi");

            migrationBuilder.DropForeignKey(
                name: "FK_PontoTaxi_Users_UpdateUserId",
                table: "PontoTaxi");

            migrationBuilder.DropForeignKey(
                name: "FK_Rota_Users_DeleteUserId",
                table: "Rota");

            migrationBuilder.DropForeignKey(
                name: "FK_Rota_Users_InsertUserId",
                table: "Rota");

            migrationBuilder.DropForeignKey(
                name: "FK_Rota_Users_UpdateUserId",
                table: "Rota");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoCorrida_Users_DeleteUserId",
                table: "SolicitacaoCorrida");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoCorrida_Users_InsertUserId",
                table: "SolicitacaoCorrida");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoCorrida_Users_UpdateUserId",
                table: "SolicitacaoCorrida");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoCorridaTaxista_Users_DeleteUserId",
                table: "SolicitacaoCorridaTaxista");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoCorridaTaxista_Users_InsertUserId",
                table: "SolicitacaoCorridaTaxista");

            migrationBuilder.DropForeignKey(
                name: "FK_SolicitacaoCorridaTaxista_Users_UpdateUserId",
                table: "SolicitacaoCorridaTaxista");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarifa_Users_DeleteUserId",
                table: "Tarifa");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarifa_Users_InsertUserId",
                table: "Tarifa");

            migrationBuilder.DropForeignKey(
                name: "FK_Tarifa_Users_UpdateUserId",
                table: "Tarifa");

            migrationBuilder.DropForeignKey(
                name: "FK_Taxista_Users_DeleteUserId",
                table: "Taxista");

            migrationBuilder.DropForeignKey(
                name: "FK_Taxista_Users_InsertUserId",
                table: "Taxista");

            migrationBuilder.DropForeignKey(
                name: "FK_Taxista_Users_UpdateUserId",
                table: "Taxista");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioGrupoUsuario_Users_DeleteUserId",
                table: "UsuarioGrupoUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioGrupoUsuario_Users_InsertUserId",
                table: "UsuarioGrupoUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioGrupoUsuario_Users_UpdateUserId",
                table: "UsuarioGrupoUsuario");

            migrationBuilder.DropForeignKey(
                name: "FK_Veiculo_Users_DeleteUserId",
                table: "Veiculo");

            migrationBuilder.DropForeignKey(
                name: "FK_Veiculo_Users_InsertUserId",
                table: "Veiculo");

            migrationBuilder.DropForeignKey(
                name: "FK_Veiculo_Users_UpdateUserId",
                table: "Veiculo");

            migrationBuilder.DropForeignKey(
                name: "FK_VeiculoTaxista_Users_DeleteUserId",
                table: "VeiculoTaxista");

            migrationBuilder.DropForeignKey(
                name: "FK_VeiculoTaxista_Users_InsertUserId",
                table: "VeiculoTaxista");

            migrationBuilder.DropForeignKey(
                name: "FK_VeiculoTaxista_Users_UpdateUserId",
                table: "VeiculoTaxista");

            migrationBuilder.DropIndex(
                name: "IX_VeiculoTaxista_DeleteUserId",
                table: "VeiculoTaxista");

            migrationBuilder.DropIndex(
                name: "IX_VeiculoTaxista_InsertUserId",
                table: "VeiculoTaxista");

            migrationBuilder.DropIndex(
                name: "IX_VeiculoTaxista_UpdateUserId",
                table: "VeiculoTaxista");

            migrationBuilder.DropIndex(
                name: "IX_Veiculo_DeleteUserId",
                table: "Veiculo");

            migrationBuilder.DropIndex(
                name: "IX_Veiculo_InsertUserId",
                table: "Veiculo");

            migrationBuilder.DropIndex(
                name: "IX_Veiculo_UpdateUserId",
                table: "Veiculo");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioGrupoUsuario_DeleteUserId",
                table: "UsuarioGrupoUsuario");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioGrupoUsuario_InsertUserId",
                table: "UsuarioGrupoUsuario");

            migrationBuilder.DropIndex(
                name: "IX_UsuarioGrupoUsuario_UpdateUserId",
                table: "UsuarioGrupoUsuario");

            migrationBuilder.DropIndex(
                name: "IX_Taxista_DeleteUserId",
                table: "Taxista");

            migrationBuilder.DropIndex(
                name: "IX_Taxista_InsertUserId",
                table: "Taxista");

            migrationBuilder.DropIndex(
                name: "IX_Taxista_UpdateUserId",
                table: "Taxista");

            migrationBuilder.DropIndex(
                name: "IX_Tarifa_DeleteUserId",
                table: "Tarifa");

            migrationBuilder.DropIndex(
                name: "IX_Tarifa_InsertUserId",
                table: "Tarifa");

            migrationBuilder.DropIndex(
                name: "IX_Tarifa_UpdateUserId",
                table: "Tarifa");

            migrationBuilder.DropIndex(
                name: "IX_SolicitacaoCorridaTaxista_DeleteUserId",
                table: "SolicitacaoCorridaTaxista");

            migrationBuilder.DropIndex(
                name: "IX_SolicitacaoCorridaTaxista_InsertUserId",
                table: "SolicitacaoCorridaTaxista");

            migrationBuilder.DropIndex(
                name: "IX_SolicitacaoCorridaTaxista_UpdateUserId",
                table: "SolicitacaoCorridaTaxista");

            migrationBuilder.DropIndex(
                name: "IX_SolicitacaoCorrida_DeleteUserId",
                table: "SolicitacaoCorrida");

            migrationBuilder.DropIndex(
                name: "IX_SolicitacaoCorrida_InsertUserId",
                table: "SolicitacaoCorrida");

            migrationBuilder.DropIndex(
                name: "IX_SolicitacaoCorrida_UpdateUserId",
                table: "SolicitacaoCorrida");

            migrationBuilder.DropIndex(
                name: "IX_Rota_DeleteUserId",
                table: "Rota");

            migrationBuilder.DropIndex(
                name: "IX_Rota_InsertUserId",
                table: "Rota");

            migrationBuilder.DropIndex(
                name: "IX_Rota_UpdateUserId",
                table: "Rota");

            migrationBuilder.DropIndex(
                name: "IX_PontoTaxi_DeleteUserId",
                table: "PontoTaxi");

            migrationBuilder.DropIndex(
                name: "IX_PontoTaxi_InsertUserId",
                table: "PontoTaxi");

            migrationBuilder.DropIndex(
                name: "IX_PontoTaxi_UpdateUserId",
                table: "PontoTaxi");

            migrationBuilder.DropIndex(
                name: "IX_Passageiro_DeleteUserId",
                table: "Passageiro");

            migrationBuilder.DropIndex(
                name: "IX_Passageiro_InsertUserId",
                table: "Passageiro");

            migrationBuilder.DropIndex(
                name: "IX_Passageiro_UpdateUserId",
                table: "Passageiro");

            migrationBuilder.DropIndex(
                name: "IX_MensagemDestinatario_DeleteUserId",
                table: "MensagemDestinatario");

            migrationBuilder.DropIndex(
                name: "IX_MensagemDestinatario_InsertUserId",
                table: "MensagemDestinatario");

            migrationBuilder.DropIndex(
                name: "IX_MensagemDestinatario_UpdateUserId",
                table: "MensagemDestinatario");

            migrationBuilder.DropIndex(
                name: "IX_Mensagem_DeleteUserId",
                table: "Mensagem");

            migrationBuilder.DropIndex(
                name: "IX_Mensagem_InsertUserId",
                table: "Mensagem");

            migrationBuilder.DropIndex(
                name: "IX_Mensagem_UpdateUserId",
                table: "Mensagem");

            migrationBuilder.DropIndex(
                name: "IX_Localizacao_DeleteUserId",
                table: "Localizacao");

            migrationBuilder.DropIndex(
                name: "IX_Localizacao_InsertUserId",
                table: "Localizacao");

            migrationBuilder.DropIndex(
                name: "IX_Localizacao_UpdateUserId",
                table: "Localizacao");

            migrationBuilder.DropIndex(
                name: "IX_GrupoUsuario_DeleteUserId",
                table: "GrupoUsuario");

            migrationBuilder.DropIndex(
                name: "IX_GrupoUsuario_InsertUserId",
                table: "GrupoUsuario");

            migrationBuilder.DropIndex(
                name: "IX_GrupoUsuario_UpdateUserId",
                table: "GrupoUsuario");

            migrationBuilder.DropIndex(
                name: "IX_Foto_DeleteUserId",
                table: "Foto");

            migrationBuilder.DropIndex(
                name: "IX_Foto_InsertUserId",
                table: "Foto");

            migrationBuilder.DropIndex(
                name: "IX_Foto_UpdateUserId",
                table: "Foto");

            migrationBuilder.DropIndex(
                name: "IX_FormaPagamentoTaxista_DeleteUserId",
                table: "FormaPagamentoTaxista");

            migrationBuilder.DropIndex(
                name: "IX_FormaPagamentoTaxista_InsertUserId",
                table: "FormaPagamentoTaxista");

            migrationBuilder.DropIndex(
                name: "IX_FormaPagamentoTaxista_UpdateUserId",
                table: "FormaPagamentoTaxista");

            migrationBuilder.DropIndex(
                name: "IX_FormaPagamento_DeleteUserId",
                table: "FormaPagamento");

            migrationBuilder.DropIndex(
                name: "IX_FormaPagamento_InsertUserId",
                table: "FormaPagamento");

            migrationBuilder.DropIndex(
                name: "IX_FormaPagamento_UpdateUserId",
                table: "FormaPagamento");

            migrationBuilder.DropIndex(
                name: "IX_Favorito_DeleteUserId",
                table: "Favorito");

            migrationBuilder.DropIndex(
                name: "IX_Favorito_InsertUserId",
                table: "Favorito");

            migrationBuilder.DropIndex(
                name: "IX_Favorito_UpdateUserId",
                table: "Favorito");

            migrationBuilder.DropIndex(
                name: "IX_FaturamentoTaxista_DeleteUserId",
                table: "FaturamentoTaxista");

            migrationBuilder.DropIndex(
                name: "IX_FaturamentoTaxista_InsertUserId",
                table: "FaturamentoTaxista");

            migrationBuilder.DropIndex(
                name: "IX_FaturamentoTaxista_UpdateUserId",
                table: "FaturamentoTaxista");

            migrationBuilder.DropIndex(
                name: "IX_Faturamento_DeleteUserId",
                table: "Faturamento");

            migrationBuilder.DropIndex(
                name: "IX_Faturamento_InsertUserId",
                table: "Faturamento");

            migrationBuilder.DropIndex(
                name: "IX_Faturamento_UpdateUserId",
                table: "Faturamento");

            migrationBuilder.DropIndex(
                name: "IX_FaixaDescontoTaxista_DeleteUserId",
                table: "FaixaDescontoTaxista");

            migrationBuilder.DropIndex(
                name: "IX_FaixaDescontoTaxista_InsertUserId",
                table: "FaixaDescontoTaxista");

            migrationBuilder.DropIndex(
                name: "IX_FaixaDescontoTaxista_UpdateUserId",
                table: "FaixaDescontoTaxista");

            migrationBuilder.DropIndex(
                name: "IX_FaixaDesconto_DeleteUserId",
                table: "FaixaDesconto");

            migrationBuilder.DropIndex(
                name: "IX_FaixaDesconto_InsertUserId",
                table: "FaixaDesconto");

            migrationBuilder.DropIndex(
                name: "IX_FaixaDesconto_UpdateUserId",
                table: "FaixaDesconto");

            migrationBuilder.DropIndex(
                name: "IX_Endereco_DeleteUserId",
                table: "Endereco");

            migrationBuilder.DropIndex(
                name: "IX_Endereco_InsertUserId",
                table: "Endereco");

            migrationBuilder.DropIndex(
                name: "IX_Endereco_UpdateUserId",
                table: "Endereco");

            migrationBuilder.DropIndex(
                name: "IX_Emergencias_DeleteUserId",
                table: "Emergencias");

            migrationBuilder.DropIndex(
                name: "IX_Emergencias_InsertUserId",
                table: "Emergencias");

            migrationBuilder.DropIndex(
                name: "IX_Emergencias_UpdateUserId",
                table: "Emergencias");

            migrationBuilder.DropIndex(
                name: "IX_CorVeiculo_DeleteUserId",
                table: "CorVeiculo");

            migrationBuilder.DropIndex(
                name: "IX_CorVeiculo_InsertUserId",
                table: "CorVeiculo");

            migrationBuilder.DropIndex(
                name: "IX_CorVeiculo_UpdateUserId",
                table: "CorVeiculo");

            migrationBuilder.DropIndex(
                name: "IX_Corrida_DeleteUserId",
                table: "Corrida");

            migrationBuilder.DropIndex(
                name: "IX_Corrida_InsertUserId",
                table: "Corrida");

            migrationBuilder.DropIndex(
                name: "IX_Corrida_UpdateUserId",
                table: "Corrida");

            migrationBuilder.DropIndex(
                name: "IX_Contrato_DeleteUserId",
                table: "Contrato");

            migrationBuilder.DropIndex(
                name: "IX_Contrato_InsertUserId",
                table: "Contrato");

            migrationBuilder.DropIndex(
                name: "IX_Contrato_UpdateUserId",
                table: "Contrato");

            migrationBuilder.DropIndex(
                name: "IX_Contato_DeleteUserId",
                table: "Contato");

            migrationBuilder.DropIndex(
                name: "IX_Contato_InsertUserId",
                table: "Contato");

            migrationBuilder.DropIndex(
                name: "IX_Contato_UpdateUserId",
                table: "Contato");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "VeiculoTaxista");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "VeiculoTaxista");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "VeiculoTaxista");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Veiculo");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "UsuarioGrupoUsuario");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "UsuarioGrupoUsuario");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "UsuarioGrupoUsuario");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "Taxista");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "Taxista");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Taxista");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "Tarifa");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "Tarifa");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Tarifa");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "SolicitacaoCorridaTaxista");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "SolicitacaoCorridaTaxista");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "SolicitacaoCorridaTaxista");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "SolicitacaoCorrida");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "SolicitacaoCorrida");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "SolicitacaoCorrida");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "Rota");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "Rota");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Rota");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "PontoTaxi");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "PontoTaxi");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "PontoTaxi");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "Passageiro");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "Passageiro");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Passageiro");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "MensagemDestinatario");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "MensagemDestinatario");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "MensagemDestinatario");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "Mensagem");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "Mensagem");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Mensagem");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "Localizacao");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "Localizacao");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Localizacao");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "GrupoUsuario");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "GrupoUsuario");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "GrupoUsuario");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "Foto");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "Foto");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Foto");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "FormaPagamentoTaxista");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "FormaPagamentoTaxista");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "FormaPagamentoTaxista");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "FormaPagamento");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "FormaPagamento");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "FormaPagamento");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "Favorito");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "Favorito");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Favorito");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "FaturamentoTaxista");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "FaturamentoTaxista");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "FaturamentoTaxista");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "Faturamento");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "Faturamento");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Faturamento");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "FaixaDescontoTaxista");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "FaixaDescontoTaxista");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "FaixaDescontoTaxista");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "FaixaDesconto");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "FaixaDesconto");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "FaixaDesconto");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Endereco");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "Emergencias");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "Emergencias");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Emergencias");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "CorVeiculo");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "CorVeiculo");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "CorVeiculo");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "Corrida");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "Corrida");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Corrida");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Contrato");

            migrationBuilder.DropColumn(
                name: "DeleteUserId",
                table: "Contato");

            migrationBuilder.DropColumn(
                name: "InsertUserId",
                table: "Contato");

            migrationBuilder.DropColumn(
                name: "UpdateUserId",
                table: "Contato");

            migrationBuilder.AddColumn<int>(
                name: "NumeroIdentificacao",
                table: "Taxista",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroIdentificacao",
                table: "Taxista");

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "VeiculoTaxista",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "VeiculoTaxista",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "VeiculoTaxista",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "Veiculo",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "Veiculo",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "Veiculo",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "UsuarioGrupoUsuario",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "UsuarioGrupoUsuario",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "UsuarioGrupoUsuario",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "Taxista",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "Taxista",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "Taxista",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "Tarifa",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "Tarifa",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "Tarifa",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "SolicitacaoCorridaTaxista",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "SolicitacaoCorridaTaxista",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "SolicitacaoCorridaTaxista",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "SolicitacaoCorrida",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "SolicitacaoCorrida",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "SolicitacaoCorrida",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "Rota",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "Rota",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "Rota",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "PontoTaxi",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "PontoTaxi",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "PontoTaxi",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "Passageiro",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "Passageiro",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "Passageiro",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "MensagemDestinatario",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "MensagemDestinatario",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "MensagemDestinatario",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "Mensagem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "Mensagem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "Mensagem",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "Localizacao",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "Localizacao",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "Localizacao",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "GrupoUsuario",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "GrupoUsuario",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "GrupoUsuario",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "Foto",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "Foto",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "Foto",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "FormaPagamentoTaxista",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "FormaPagamentoTaxista",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "FormaPagamentoTaxista",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "FormaPagamento",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "FormaPagamento",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "FormaPagamento",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "Favorito",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "Favorito",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "Favorito",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "FaturamentoTaxista",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "FaturamentoTaxista",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "FaturamentoTaxista",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "Faturamento",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "Faturamento",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "Faturamento",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "FaixaDescontoTaxista",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "FaixaDescontoTaxista",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "FaixaDescontoTaxista",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "FaixaDesconto",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "FaixaDesconto",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "FaixaDesconto",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "Endereco",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "Endereco",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "Endereco",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "Emergencias",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "Emergencias",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "Emergencias",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "CorVeiculo",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "CorVeiculo",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "CorVeiculo",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "Corrida",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "Corrida",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "Corrida",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "Contrato",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "Contrato",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "Contrato",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleteUserId",
                table: "Contato",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InsertUserId",
                table: "Contato",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateUserId",
                table: "Contato",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VeiculoTaxista_DeleteUserId",
                table: "VeiculoTaxista",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VeiculoTaxista_InsertUserId",
                table: "VeiculoTaxista",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_VeiculoTaxista_UpdateUserId",
                table: "VeiculoTaxista",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_DeleteUserId",
                table: "Veiculo",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_InsertUserId",
                table: "Veiculo",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Veiculo_UpdateUserId",
                table: "Veiculo",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioGrupoUsuario_DeleteUserId",
                table: "UsuarioGrupoUsuario",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioGrupoUsuario_InsertUserId",
                table: "UsuarioGrupoUsuario",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioGrupoUsuario_UpdateUserId",
                table: "UsuarioGrupoUsuario",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Taxista_DeleteUserId",
                table: "Taxista",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Taxista_InsertUserId",
                table: "Taxista",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Taxista_UpdateUserId",
                table: "Taxista",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarifa_DeleteUserId",
                table: "Tarifa",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarifa_InsertUserId",
                table: "Tarifa",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tarifa_UpdateUserId",
                table: "Tarifa",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorridaTaxista_DeleteUserId",
                table: "SolicitacaoCorridaTaxista",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorridaTaxista_InsertUserId",
                table: "SolicitacaoCorridaTaxista",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorridaTaxista_UpdateUserId",
                table: "SolicitacaoCorridaTaxista",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorrida_DeleteUserId",
                table: "SolicitacaoCorrida",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorrida_InsertUserId",
                table: "SolicitacaoCorrida",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoCorrida_UpdateUserId",
                table: "SolicitacaoCorrida",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rota_DeleteUserId",
                table: "Rota",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rota_InsertUserId",
                table: "Rota",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rota_UpdateUserId",
                table: "Rota",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PontoTaxi_DeleteUserId",
                table: "PontoTaxi",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PontoTaxi_InsertUserId",
                table: "PontoTaxi",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PontoTaxi_UpdateUserId",
                table: "PontoTaxi",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Passageiro_DeleteUserId",
                table: "Passageiro",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Passageiro_InsertUserId",
                table: "Passageiro",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Passageiro_UpdateUserId",
                table: "Passageiro",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MensagemDestinatario_DeleteUserId",
                table: "MensagemDestinatario",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MensagemDestinatario_InsertUserId",
                table: "MensagemDestinatario",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MensagemDestinatario_UpdateUserId",
                table: "MensagemDestinatario",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_DeleteUserId",
                table: "Mensagem",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_InsertUserId",
                table: "Mensagem",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagem_UpdateUserId",
                table: "Mensagem",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Localizacao_DeleteUserId",
                table: "Localizacao",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Localizacao_InsertUserId",
                table: "Localizacao",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Localizacao_UpdateUserId",
                table: "Localizacao",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoUsuario_DeleteUserId",
                table: "GrupoUsuario",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoUsuario_InsertUserId",
                table: "GrupoUsuario",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoUsuario_UpdateUserId",
                table: "GrupoUsuario",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Foto_DeleteUserId",
                table: "Foto",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Foto_InsertUserId",
                table: "Foto",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Foto_UpdateUserId",
                table: "Foto",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FormaPagamentoTaxista_DeleteUserId",
                table: "FormaPagamentoTaxista",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FormaPagamentoTaxista_InsertUserId",
                table: "FormaPagamentoTaxista",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FormaPagamentoTaxista_UpdateUserId",
                table: "FormaPagamentoTaxista",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FormaPagamento_DeleteUserId",
                table: "FormaPagamento",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FormaPagamento_InsertUserId",
                table: "FormaPagamento",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FormaPagamento_UpdateUserId",
                table: "FormaPagamento",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorito_DeleteUserId",
                table: "Favorito",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorito_InsertUserId",
                table: "Favorito",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorito_UpdateUserId",
                table: "Favorito",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoTaxista_DeleteUserId",
                table: "FaturamentoTaxista",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoTaxista_InsertUserId",
                table: "FaturamentoTaxista",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaturamentoTaxista_UpdateUserId",
                table: "FaturamentoTaxista",
                column: "UpdateUserId");

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
                name: "IX_FaixaDescontoTaxista_DeleteUserId",
                table: "FaixaDescontoTaxista",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaixaDescontoTaxista_InsertUserId",
                table: "FaixaDescontoTaxista",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaixaDescontoTaxista_UpdateUserId",
                table: "FaixaDescontoTaxista",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaixaDesconto_DeleteUserId",
                table: "FaixaDesconto",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaixaDesconto_InsertUserId",
                table: "FaixaDesconto",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaixaDesconto_UpdateUserId",
                table: "FaixaDesconto",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_DeleteUserId",
                table: "Endereco",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_InsertUserId",
                table: "Endereco",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Endereco_UpdateUserId",
                table: "Endereco",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Emergencias_DeleteUserId",
                table: "Emergencias",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Emergencias_InsertUserId",
                table: "Emergencias",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Emergencias_UpdateUserId",
                table: "Emergencias",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CorVeiculo_DeleteUserId",
                table: "CorVeiculo",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CorVeiculo_InsertUserId",
                table: "CorVeiculo",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CorVeiculo_UpdateUserId",
                table: "CorVeiculo",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Corrida_DeleteUserId",
                table: "Corrida",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Corrida_InsertUserId",
                table: "Corrida",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Corrida_UpdateUserId",
                table: "Corrida",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_DeleteUserId",
                table: "Contrato",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_InsertUserId",
                table: "Contrato",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contrato_UpdateUserId",
                table: "Contrato",
                column: "UpdateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contato_DeleteUserId",
                table: "Contato",
                column: "DeleteUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contato_InsertUserId",
                table: "Contato",
                column: "InsertUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contato_UpdateUserId",
                table: "Contato",
                column: "UpdateUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contato_Users_DeleteUserId",
                table: "Contato",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contato_Users_InsertUserId",
                table: "Contato",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contato_Users_UpdateUserId",
                table: "Contato",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Users_DeleteUserId",
                table: "Contrato",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Users_InsertUserId",
                table: "Contrato",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contrato_Users_UpdateUserId",
                table: "Contrato",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Corrida_Users_DeleteUserId",
                table: "Corrida",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Corrida_Users_InsertUserId",
                table: "Corrida",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Corrida_Users_UpdateUserId",
                table: "Corrida",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CorVeiculo_Users_DeleteUserId",
                table: "CorVeiculo",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CorVeiculo_Users_InsertUserId",
                table: "CorVeiculo",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CorVeiculo_Users_UpdateUserId",
                table: "CorVeiculo",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Emergencias_Users_DeleteUserId",
                table: "Emergencias",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Emergencias_Users_InsertUserId",
                table: "Emergencias",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Emergencias_Users_UpdateUserId",
                table: "Emergencias",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Users_DeleteUserId",
                table: "Endereco",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Users_InsertUserId",
                table: "Endereco",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Endereco_Users_UpdateUserId",
                table: "Endereco",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FaixaDesconto_Users_DeleteUserId",
                table: "FaixaDesconto",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FaixaDesconto_Users_InsertUserId",
                table: "FaixaDesconto",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FaixaDesconto_Users_UpdateUserId",
                table: "FaixaDesconto",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FaixaDescontoTaxista_Users_DeleteUserId",
                table: "FaixaDescontoTaxista",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FaixaDescontoTaxista_Users_InsertUserId",
                table: "FaixaDescontoTaxista",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FaixaDescontoTaxista_Users_UpdateUserId",
                table: "FaixaDescontoTaxista",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Faturamento_Users_DeleteUserId",
                table: "Faturamento",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Faturamento_Users_InsertUserId",
                table: "Faturamento",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Faturamento_Users_UpdateUserId",
                table: "Faturamento",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FaturamentoTaxista_Users_DeleteUserId",
                table: "FaturamentoTaxista",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FaturamentoTaxista_Users_InsertUserId",
                table: "FaturamentoTaxista",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FaturamentoTaxista_Users_UpdateUserId",
                table: "FaturamentoTaxista",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorito_Users_DeleteUserId",
                table: "Favorito",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorito_Users_InsertUserId",
                table: "Favorito",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorito_Users_UpdateUserId",
                table: "Favorito",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FormaPagamento_Users_DeleteUserId",
                table: "FormaPagamento",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FormaPagamento_Users_InsertUserId",
                table: "FormaPagamento",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FormaPagamento_Users_UpdateUserId",
                table: "FormaPagamento",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FormaPagamentoTaxista_Users_DeleteUserId",
                table: "FormaPagamentoTaxista",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FormaPagamentoTaxista_Users_InsertUserId",
                table: "FormaPagamentoTaxista",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FormaPagamentoTaxista_Users_UpdateUserId",
                table: "FormaPagamentoTaxista",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Foto_Users_DeleteUserId",
                table: "Foto",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Foto_Users_InsertUserId",
                table: "Foto",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Foto_Users_UpdateUserId",
                table: "Foto",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GrupoUsuario_Users_DeleteUserId",
                table: "GrupoUsuario",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GrupoUsuario_Users_InsertUserId",
                table: "GrupoUsuario",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GrupoUsuario_Users_UpdateUserId",
                table: "GrupoUsuario",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Localizacao_Users_DeleteUserId",
                table: "Localizacao",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Localizacao_Users_InsertUserId",
                table: "Localizacao",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Localizacao_Users_UpdateUserId",
                table: "Localizacao",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Mensagem_Users_DeleteUserId",
                table: "Mensagem",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Mensagem_Users_InsertUserId",
                table: "Mensagem",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Mensagem_Users_UpdateUserId",
                table: "Mensagem",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MensagemDestinatario_Users_DeleteUserId",
                table: "MensagemDestinatario",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MensagemDestinatario_Users_InsertUserId",
                table: "MensagemDestinatario",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MensagemDestinatario_Users_UpdateUserId",
                table: "MensagemDestinatario",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Passageiro_Users_DeleteUserId",
                table: "Passageiro",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Passageiro_Users_InsertUserId",
                table: "Passageiro",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Passageiro_Users_UpdateUserId",
                table: "Passageiro",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PontoTaxi_Users_DeleteUserId",
                table: "PontoTaxi",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PontoTaxi_Users_InsertUserId",
                table: "PontoTaxi",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PontoTaxi_Users_UpdateUserId",
                table: "PontoTaxi",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rota_Users_DeleteUserId",
                table: "Rota",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rota_Users_InsertUserId",
                table: "Rota",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rota_Users_UpdateUserId",
                table: "Rota",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoCorrida_Users_DeleteUserId",
                table: "SolicitacaoCorrida",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoCorrida_Users_InsertUserId",
                table: "SolicitacaoCorrida",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoCorrida_Users_UpdateUserId",
                table: "SolicitacaoCorrida",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoCorridaTaxista_Users_DeleteUserId",
                table: "SolicitacaoCorridaTaxista",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoCorridaTaxista_Users_InsertUserId",
                table: "SolicitacaoCorridaTaxista",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SolicitacaoCorridaTaxista_Users_UpdateUserId",
                table: "SolicitacaoCorridaTaxista",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tarifa_Users_DeleteUserId",
                table: "Tarifa",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tarifa_Users_InsertUserId",
                table: "Tarifa",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tarifa_Users_UpdateUserId",
                table: "Tarifa",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Taxista_Users_DeleteUserId",
                table: "Taxista",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Taxista_Users_InsertUserId",
                table: "Taxista",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Taxista_Users_UpdateUserId",
                table: "Taxista",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioGrupoUsuario_Users_DeleteUserId",
                table: "UsuarioGrupoUsuario",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioGrupoUsuario_Users_InsertUserId",
                table: "UsuarioGrupoUsuario",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioGrupoUsuario_Users_UpdateUserId",
                table: "UsuarioGrupoUsuario",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculo_Users_DeleteUserId",
                table: "Veiculo",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculo_Users_InsertUserId",
                table: "Veiculo",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Veiculo_Users_UpdateUserId",
                table: "Veiculo",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VeiculoTaxista_Users_DeleteUserId",
                table: "VeiculoTaxista",
                column: "DeleteUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VeiculoTaxista_Users_InsertUserId",
                table: "VeiculoTaxista",
                column: "InsertUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VeiculoTaxista_Users_UpdateUserId",
                table: "VeiculoTaxista",
                column: "UpdateUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using CloudMe.ToDeTaxi.Domain.Enums;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class SolicitacaoCorridaRepository : BaseRepository<SolicitacaoCorrida>, ISolicitacaoCorridaRepository
    {
        public SolicitacaoCorridaRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }

        public async Task<int> ObterNumeroAceitacoes(SolicitacaoCorrida solicitacao)
        {
            return await Context.Set<SolicitacaoCorridaTaxista>()
                .Where(sol_tx =>
                    sol_tx.IdSolicitacaoCorrida == solicitacao.Id &&
                    sol_tx.Acao == AcaoTaxistaSolicitacaoCorrida.Aceita).CountAsync();
        }

        public async Task<bool> RegistrarAcaoTaxista(SolicitacaoCorrida solicitacao, Taxista taxista, AcaoTaxistaSolicitacaoCorrida acao)
        {
            SolicitacaoCorridaTaxista solTx = new SolicitacaoCorridaTaxista()
            {
                IdTaxista = taxista.Id,
                IdSolicitacaoCorrida = solicitacao.Id,
                Acao = acao
            };

            Context.Entry(solTx).State = EntityState.Added;

            return await Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AlterarStatusMonitoramento(SolicitacaoCorrida solicitacao, StatusMonitoramentoSolicitacaoCorrida status)
        {
            solicitacao.StatusMonitoramento = status;
            Context.Entry(solicitacao).State = EntityState.Modified;
            return await Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AlterarSituacao(SolicitacaoCorrida solicitacao, SituacaoSolicitacaoCorrida situacao)
        {
            solicitacao.Situacao = situacao;
            Context.Entry(solicitacao).State = EntityState.Modified;
            return await Context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Taxista>> ClassificarTaxistas(SolicitacaoCorrida solicitacao)
        {
            var taxistas = Context.Set<Taxista>()
                .Include(x => x.FormasPagamento)
                .Include(x => x.FaixasDesconto)
                .Include(x => x.LocalizacaoAtual)
                .Include(x => x.Veiculos)
                //.Include(x => x.Favoritos)
                .Include(x => x.SolicitacoesCorrida)
                .Where(x =>
                    x.Ativo && // taxista que está ativo
                    x.Disponivel && // ... que está disponível
                    x.FormasPagamento.Any(frmPgto => frmPgto.IdFormaPagamento == solicitacao.IdFormaPagamento) && // ... que aceita a forma de pagamento da solicitação
                    x.FaixasDesconto.Any(fxDesc => fxDesc.IdFaixaDesconto == solicitacao.IdFaixaDesconto || !solicitacao.IdFaixaDesconto.HasValue) && // ... que adota a faixa de desconto solicitada
                    x.Veiculos.Any(veicTx => veicTx.Ativo) &&   // ... que está utilizando um veículo no momento
                    x.SolicitacoesCorrida.Any(
                        solCorrTx => solCorrTx.IdSolicitacaoCorrida == solicitacao.Id &&
                        solCorrTx.Acao == AcaoTaxistaSolicitacaoCorrida.Aceita)); // ... que participou do pregão da solicitação */

            var favoritos = Context.Set<Favorito>()
                .Where(fav => fav.IdPassageiro == solicitacao.IdPassageiro);

            var taxistas_com_favoritos =
                from tx in taxistas
                join favorito in favoritos on tx.Id equals favorito.IdTaxista into tx_fav_join
                from tx_fav in tx_fav_join.DefaultIfEmpty()
                select new
                {
                    taxista = tx,
                    distancia = SolicitacaoCorrida.ObterDistancia(solicitacao.LocalizacaoOrigem, tx.LocalizacaoAtual),
                    pref_favorito = tx_fav != null ? tx_fav.Preferencia : 0
                };

            //orderby tx_fav.Preferencia, SolicitacaoCorrida.ObterDistancia(solicitacao.LocalizacaoOrigem, taxista.LocalizacaoAtual)
            //select taxista;

            var resultado =
                from tx_fav in taxistas_com_favoritos
                orderby tx_fav.pref_favorito, tx_fav.distancia
                select tx_fav.taxista;

            return await resultado.Distinct().ToListAsync();
        }
    }
}

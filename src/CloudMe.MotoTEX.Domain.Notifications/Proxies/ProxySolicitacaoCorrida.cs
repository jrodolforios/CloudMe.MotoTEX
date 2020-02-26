using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudMe.MotoTEX.Domain.Enums;
using CloudMe.MotoTEX.Domain.Model.Corrida;
using CloudMe.MotoTEX.Domain.Notifications.Abstract;
using CloudMe.MotoTEX.Domain.Notifications.Abstract.Proxies;
using CloudMe.MotoTEX.Domain.Notifications.Hubs;
using CloudMe.MotoTEX.Infraestructure.Entries;
using Microsoft.AspNetCore.SignalR;

namespace CloudMe.MotoTEX.Domain.Notifications.Proxies
{
    public class ProxySolicitacaoCorrida : IProxySolicitacaoCorrida
    {
        IHubContext<HubNotificacoes> hubContext;
        IHubContext<HubNotificaoesAdmin> hubContextAdmin;
        IFirebaseNotifications firebaseNotifications;

        public ProxySolicitacaoCorrida(IHubContext<HubNotificacoes> hubContext, IHubContext<HubNotificaoesAdmin> hubContextAdmin, IFirebaseNotifications firebaseNotifications)
        {
            this.hubContext = hubContext;
            this.hubContextAdmin = hubContextAdmin;
            this.firebaseNotifications = firebaseNotifications;
        }

        public async Task AtivarTaxista(Taxista taxista, SolicitacaoCorridaSummary solicitacaoCorrida)
        {
            bool online = taxista.Online();
            if (online)
            {
                await hubContext.Clients.User(taxista.IdUsuario.ToString()).SendAsync("sol_corr_ativar_tx", solicitacaoCorrida);
            }
            else
            {
                await firebaseNotifications.SendPushNotification(
                    new[] { taxista.Usuario },
                    "Passageiro próximo solicitando Corrida",
                    "Um passageiro está solicitando uma corrida, fique ativo para receber solicitações de corrida!",
                    solicitacaoCorrida);
            }

            await hubContextAdmin.Clients.All.SendAsync(online ? "sol_corr_ativ_tx_hub" : "sol_corr_ativ_tx_push", new
            {
                taxistas = (new[] { taxista.Id }).AsEnumerable(),
                sol_corr = solicitacaoCorrida
            });
        }

        public async Task AtivarTaxistas(IEnumerable<Taxista> taxistas, SolicitacaoCorridaSummary solicitacaoCorrida)
        {
            var txsOnline = taxistas.Where(tx => tx.Online());
            var txsOffline = taxistas.Except(txsOnline);
      
            if (txsOnline.Count() > 0)
            {
                // taxistas online recebem a solicitação por signalr
                await hubContext.Clients.Users(txsOnline.Select(x => x.IdUsuario.ToString()).ToList()).SendAsync("sol_corr_ativar_tx", solicitacaoCorrida);
                await hubContextAdmin.Clients.All.SendAsync("sol_corr_ativ_tx_hub", new
                {
                    taxistas = txsOnline.Select(x => x.Id),
                    sol_corr = solicitacaoCorrida
                });
            }

            if (txsOffline.Count() > 0)
            {
                // taxistas online recebem a solicitação por push notification
                await firebaseNotifications.SendPushNotification(
                    txsOffline.Select(tx => tx.Usuario),
                    "Passageiro próximo solicitando Corrida",
                    "Um passageiro está solicitando uma corrida, fique ativo para receber solicitações de corrida!",
                    solicitacaoCorrida);

                await hubContextAdmin.Clients.All.SendAsync("sol_corr_ativ_tx_push", new
                {
                    taxistas = txsOffline.Select(x => x.Id),
                    sol_corr = solicitacaoCorrida
                });
            }

        }

        public async Task InformarAcaoTaxista(Taxista taxista, SolicitacaoCorrida solicitacao, AcaoTaxistaSolicitacaoCorrida acao)
        {
            await hubContextAdmin.Clients.All.SendAsync("sol_corr_acao_tx", new
            {
                id_taxista = taxista.Id,
                id_solicitacao = solicitacao.Id,
                acao_taxista = acao
            });
        }
    }
}

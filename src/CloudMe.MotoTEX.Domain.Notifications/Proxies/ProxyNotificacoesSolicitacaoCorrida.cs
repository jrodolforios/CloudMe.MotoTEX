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
    public class ProxyNotificacoesSolicitacaoCorrida : IProxyNotificacoesSolicitacaoCorrida
    {
        IHubContext<HubNotificaoes> hubContext;
        IHubContext<HubNotificaoesAdmin> hubContextAdmin;
        IFirebaseNotifications firebaseNotifications;

        public ProxyNotificacoesSolicitacaoCorrida(IHubContext<HubNotificaoes> hubContext, IHubContext<HubNotificaoesAdmin> hubContextAdmin, IFirebaseNotifications firebaseNotifications)
        {
            this.hubContext = hubContext;
            this.hubContextAdmin = hubContextAdmin;
            this.firebaseNotifications = firebaseNotifications;
        }

        public async Task AtivarTaxista(Taxista taxista, SolicitacaoCorridaSummary solicitacaoCorrida)
        {
            await hubContext.Clients.User(taxista.IdUsuario.ToString()).SendAsync("sol_corr_ativar_tx", solicitacaoCorrida);
            await firebaseNotifications.SendPushNotification(
                new[] { taxista.Usuario },
                "Passageiro solicitando Corrida",
                "Um passageiro está solicitando uma corrida, toque nesta notificação para visualizar e aceitar a solicitação!",
                solicitacaoCorrida);

            await hubContextAdmin.Clients.All.SendAsync("sol_corr_ativacao_tx", new
            {
                taxistas = (new[] { taxista.Id }).AsEnumerable(),
                sol_corr = solicitacaoCorrida
            });
        }

        public async Task AtivarTaxistas(IEnumerable<Taxista> taxistas, SolicitacaoCorridaSummary solicitacaoCorrida)
        {
            await hubContext.Clients.Users(taxistas.Select(x => x.IdUsuario.ToString()).ToList()).SendAsync("sol_corr_ativar_tx", solicitacaoCorrida);
            await firebaseNotifications.SendPushNotification(
                taxistas.Select(tx => tx.Usuario),
                "Passageiro solicitando Corrida",
                "Um passageiro está solicitando uma corrida, toque nesta notificação para visualizar e aceitar a solicitação!",
                solicitacaoCorrida);

            await hubContextAdmin.Clients.All.SendAsync("sol_corr_ativacao_tx", new
            {
                taxistas = taxistas.Select(x => x.Id),
                sol_corr = solicitacaoCorrida
            });
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

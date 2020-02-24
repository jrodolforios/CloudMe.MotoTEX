using CloudMe.MotoTEX.Domain.Model.Mensagem;
using CloudMe.MotoTEX.Domain.Notifications.Abstract;
using CloudMe.MotoTEX.Domain.Notifications.Abstract.Proxies;
using CloudMe.MotoTEX.Domain.Notifications.Compat;
using CloudMe.MotoTEX.Domain.Notifications.Hubs;
using CloudMe.MotoTEX.Infraestructure.Entries;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Notifications.Proxies
{
    public class ProxyMensagem : IProxyMensagem
    {
        IHubContext<HubNotificacoes> hubNotificacoes;
        IFirebaseNotifications firebaseNotifications;

        IHubContext<HubMensagens> hubMensagens; // COMPAT

        public ProxyMensagem(
            IHubContext<HubNotificacoes> hubContext,
            IFirebaseNotifications firebaseNotifications,
            IHubContext<HubMensagens> hubMensagens) // COMPAT
        {
            hubNotificacoes = hubContext;
            this.firebaseNotifications = firebaseNotifications;
            this.hubMensagens = hubMensagens;
        }

        public async Task EnviarParaUsuario(Usuario usuario, DetalhesMensagem mensagem)
        {
            await hubNotificacoes.Clients.User(usuario.Id.ToString()).SendAsync("msg_usr", mensagem);
            await firebaseNotifications.SendPushNotification(
                new[] { usuario },
                mensagem.Assunto,
                mensagem.Corpo,
                new { });

            await hubMensagens.Clients.User(usuario.Id.ToString()).SendAsync("msg_usr", mensagem); // COMPAT
        }

        public async Task EnviarParaUsuarios(IEnumerable<Usuario> usuarios, DetalhesMensagem mensagem)
        {
            var idsUsuarios = usuarios.Select(x => x.Id.ToString()).ToList();
            await hubNotificacoes.Clients.Users(idsUsuarios).SendAsync("msg_usr", mensagem);
            await firebaseNotifications.SendPushNotification(
                usuarios,
                mensagem.Assunto,
                mensagem.Corpo,
                new { });

            await hubMensagens.Clients.Users(idsUsuarios).SendAsync("msg_usr", mensagem); // COMPAT
        }

        public async Task EnviarParaGrupoUsuarios(GrupoUsuario grupoUsuario, DetalhesMensagem mensagem)
        {
            await hubNotificacoes.Clients.Group(grupoUsuario.Id.ToString()).SendAsync("msg_grp_usr", mensagem);
            await firebaseNotifications.SendPushNotification(
                grupoUsuario,
                mensagem.Assunto,
                mensagem.Corpo,
                new { });
        }

        public async Task MensagemAtualizada(MensagemDestinatarioSummary mensagemDestinatario)
        {
            await hubNotificacoes.Clients.Users(new[]
            {
                mensagemDestinatario.IdUsuario.ToString()
            }).SendAsync("msg_upd", mensagemDestinatario);
        }

    }
}

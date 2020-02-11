using CloudMe.MotoTEX.Domain.Model.Mensagem;
using CloudMe.MotoTEX.Domain.Notifications.Abstract;
using CloudMe.MotoTEX.Domain.Notifications.Hubs;
using CloudMe.MotoTEX.Infraestructure.Entries;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Notifications
{
    public class ProxyHubMensagens : IProxyHubMensagens
    {
        IHubContext<HubMensagens> hubContext;
        IFirebaseNotifications firebaseNotifications;

        public ProxyHubMensagens(IHubContext<HubMensagens> hubContext, IFirebaseNotifications firebaseNotifications)
        {
            this.hubContext = hubContext;
            this.firebaseNotifications = firebaseNotifications;
        }

        public async Task EnviarParaUsuario(Usuario usuario, DetalhesMensagem mensagem)
        {
            await hubContext.Clients.User(usuario.Id.ToString()).SendAsync("msg_usr", mensagem);
            await firebaseNotifications.SendPushNotification(
                new[] { usuario },
                mensagem.Assunto,
                mensagem.Corpo,
                new { });
        }

        public async Task EnviarParaUsuarios(IEnumerable<Usuario> usuarios, DetalhesMensagem mensagem)
        {
            await hubContext.Clients.Users(usuarios.Select(x => x.Id.ToString()).ToList()).SendAsync("msg_usr", mensagem);
            await firebaseNotifications.SendPushNotification(
                usuarios,
                mensagem.Assunto,
                mensagem.Corpo,
                new { });
        }

        public async Task EnviarParaGrupoUsuarios(GrupoUsuario grupoUsuario, DetalhesMensagem mensagem)
        {
            await hubContext.Clients.Group(grupoUsuario.Id.ToString()).SendAsync("msg_grp_usr", mensagem);
            await firebaseNotifications.SendPushNotification(
                grupoUsuario,
                mensagem.Assunto,
                mensagem.Corpo,
                new { });
        }

        public async Task MensagemAtualizada(MensagemDestinatarioSummary mensagemDestinatario)
        {
            await hubContext.Clients.Users(new[]
            {
                mensagemDestinatario.IdUsuario.ToString()
            }).SendAsync("msg_upd", mensagemDestinatario);
        }

    }
}

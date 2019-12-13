using CloudMe.ToDeTaxi.Domain.Model.Mensagem;
using CloudMe.ToDeTaxi.Domain.Notifications.Abstract;
using CloudMe.ToDeTaxi.Domain.Notifications.Hubs;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Notifications
{
    public class ProxyHubMensagens : IProxyHubMensagens
    {
        IHubContext<HubMensagens> hubContext;

        public ProxyHubMensagens(IHubContext<HubMensagens> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task EnviarParaUsuario(Usuario usuario, DetalhesMensagem mensagem)
        {
            await hubContext.Clients.User(usuario.Id.ToString()).SendAsync("msg_usr", mensagem);
        }

        public async Task EnviarParaUsuarios(IEnumerable<Usuario> usuarios, DetalhesMensagem mensagem)
        {
            await hubContext.Clients.Users(usuarios.Select(x => x.Id.ToString()).ToList()).SendAsync("msg_usr", mensagem);
        }

        public async Task EnviarParaGrupoUsuarios(GrupoUsuario grupoUsuario, DetalhesMensagem mensagem)
        {
            await hubContext.Clients.Group(grupoUsuario.Id.ToString()).SendAsync("msg_grp_usr", mensagem);
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

using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudMe.ToDeTaxi.Domain.Notifications.Abstracts.Hubs;
using CloudMe.ToDeTaxi.Domain.Model.Mensagem;

namespace CloudMe.ToDeTaxi.Domain.Notifications.Hubs
{
    [Authorize]
    public class HubMensagens : Hub, IHubMensagens
    {
        IUsuarioRepository usuarioRepository;
        IHubContext<HubMensagens> hubContext;

        HubMensagens(IUsuarioRepository _usuarioRepository, IHubContext<HubMensagens> hubContext)
        {
            usuarioRepository = _usuarioRepository;
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

        public override async Task OnConnectedAsync()
        {
            // obtem o usuário e seus grupos
            Usuario usuario = await usuarioRepository.FindByIdAsync(Context.User.FindFirst("sub")?.Value, new[]{"Grupos"});
            if (usuario != null)
            {
                // registra o usuario nos grupos que participa
                foreach (var grupo in usuario.Grupos)
                {
                    await hubContext.Groups.AddToGroupAsync(Context.ConnectionId, grupo.Id.ToString());
                }
            }

            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}

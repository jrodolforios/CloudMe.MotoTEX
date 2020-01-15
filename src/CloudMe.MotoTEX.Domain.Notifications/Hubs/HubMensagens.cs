using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using CloudMe.MotoTEX.Infraestructure.Entries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Notifications.Hubs
{
    [Authorize]
    public class HubMensagens : UserMappedHub<HubMensagens>
    {
        public HubMensagens(IUsuarioRepository usuarioRepository, IHubContext<UserMappedHub<HubMensagens>> hubContext)
            : base(usuarioRepository, hubContext)
        {
        }

        protected override async Task UsuarioConectado(Usuario usuario)
        {
            // registra o usuario nos grupos que participa
            foreach (var grupo in usuario.Grupos)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, grupo.IdGrupoUsuario.ToString());
            }
        }

        protected override async Task UsuarioDesconectado(Usuario usuario)
        {
            // retira o usuario dos grupos que participa
            foreach (var grupo in usuario.Grupos)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, grupo.IdGrupoUsuario.ToString());
            }

            await base.UsuarioDesconectado(usuario);
        }
    }
}

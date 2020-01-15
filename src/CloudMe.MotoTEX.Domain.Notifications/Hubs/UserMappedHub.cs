using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using CloudMe.MotoTEX.Infraestructure.Entries;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Notifications.Hubs
{
    public abstract class UserMappedHub<T>: Hub where T : UserMappedHub<T>
    {
        public readonly IUsuarioRepository usuarioRepository;
        public readonly IHubContext<UserMappedHub<T>> hubContext;

        public readonly static ConnectionMapping<Guid> connections = new ConnectionMapping<Guid>();

        public UserMappedHub(IUsuarioRepository usuarioRepository, IHubContext<UserMappedHub<T>> hubContext)
        {
            this.usuarioRepository = usuarioRepository;
            this.hubContext = hubContext;
        }

        protected virtual Task UsuarioConectado(Usuario usuario) // obs.: o registro de usuário é carregado com os grupos que participa
        {
            return Task.CompletedTask;
        }

        protected virtual Task UsuarioDesconectado(Usuario usuario) // obs.: o registro de usuário é carregado com os grupos que participa
        {
            return Task.CompletedTask;
        }

        public override async Task OnConnectedAsync()
        {
            // obtem o usuário e seus grupos
            if (Guid.TryParse(Context.User.FindFirst("sub")?.Value, out Guid usrId))
            {
                Usuario usuario = await usuarioRepository.FindByIdAsync(usrId, new[] { "Grupos" });
                if (usuario != null)
                {
                    connections.Add(usuario.Id, Context.ConnectionId);
                    await UsuarioConectado(usuario);
                }
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (Guid.TryParse(Context.User.FindFirst("sub")?.Value, out Guid usrId))
            {
                Usuario usuario = await usuarioRepository.FindByIdAsync(usrId, new[] { "Grupos" });
                if (usuario != null)
                {
                    connections.Remove(usuario.Id, Context.ConnectionId);
                    await UsuarioDesconectado(usuario);
                }
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}

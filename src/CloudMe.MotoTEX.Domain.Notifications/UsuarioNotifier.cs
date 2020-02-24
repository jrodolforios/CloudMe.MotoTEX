using CloudMe.MotoTEX.Domain.Notifications.Hubs;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CloudMe.MotoTEX.Domain.Notifications
{
    public class UsuarioNotifier
    {
        IUsuarioService _usuarioService;
        IHubContext<HubNotificacoes> _hubContext = null;

        public UsuarioNotifier(IUsuarioService entryService, IHubContext<HubNotificacoes> hubContext)
        {
            _usuarioService = entryService;
            _hubContext = hubContext;

            Triggers<Usuario>.Inserted += async entry =>
            {
                var summary = await _usuarioService.GetSummaryAsync(entry.Entity);
                await _hubContext.Clients.All.SendAsync("inserted", _usuarioService.GetTag(), summary);
            };

            Triggers<Usuario>.Updated += async entry =>
            {
                var summary = await _usuarioService.GetSummaryAsync(entry.Entity);
                await _hubContext.Clients.All.SendAsync("updated", _usuarioService.GetTag(), summary);
            };

            Triggers<Usuario>.Deleted += async entry =>
            {
                await _hubContext.Clients.All.SendAsync("deleted", _usuarioService.GetTag(), entry.Entity.Id);
            };
        }
    }
}

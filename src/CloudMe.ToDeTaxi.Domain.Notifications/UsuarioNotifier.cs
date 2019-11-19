using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CloudMe.ToDeTaxi.Domain.Notifications
{
    [Authorize]
    public class UsuarioNotifier : BaseNotifier
    {
        IUsuarioService _usuarioService;
        IHubContext<UsuarioNotifier> _hubContext = null;

        public UsuarioNotifier(IUsuarioService entryService, IHubContext<UsuarioNotifier> hubContext)
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

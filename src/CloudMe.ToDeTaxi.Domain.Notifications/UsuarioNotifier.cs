using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CloudMe.ToDeTaxi.Domain.Notifications
{
    [Authorize]
    public class UsuarioNotifier<TContext> : BaseNotifier where TContext: DbContext
    {
        IUsuarioService _usuarioService;
        IHubContext<UsuarioNotifier<TContext>> _hubContext = null;

        public UsuarioNotifier(IUsuarioService entryService, IHubContext<UsuarioNotifier<TContext>> hubContext)
        {
            _usuarioService = entryService;
            _hubContext = hubContext;

            Triggers<Usuario, TContext>.Inserted += async entry =>
            {
                var summary = await _usuarioService.GetSummaryAsync(entry.Entity);
                await _hubContext.Clients.All.SendAsync("inserted", _usuarioService.GetTag(), summary);
            };

            Triggers<Usuario, TContext>.Updated += async entry =>
            {
                var summary = await _usuarioService.GetSummaryAsync(entry.Entity);
                await _hubContext.Clients.All.SendAsync("updated", _usuarioService.GetTag(), summary);
            };

            Triggers<Usuario, TContext>.Deleted += async entry =>
            {
                await _hubContext.Clients.All.SendAsync("deleted", _usuarioService.GetTag(), entry.Entity.Id);
            };
        }
    }
}

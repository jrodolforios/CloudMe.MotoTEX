using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.SignalR;

namespace CloudMe.ToDeTaxi.Domain.Notifications
{
    public class UsuarioNotifier : BaseNotifier
    {
        IUsuarioService _usuarioService;
        UsuarioNotifier(IUsuarioService entryService)
        {
            _usuarioService = entryService;

            Triggers<Usuario>.Inserted += async entry =>
            {
                var summary = await _usuarioService.GetSummaryAsync(entry.Entity);
                await Clients.All.SendAsync("inserted", _usuarioService.GetTag(), summary);
            };

            Triggers<Usuario>.Updated += async entry =>
            {
                var summary = await _usuarioService.GetSummaryAsync(entry.Entity);
                await Clients.All.SendAsync("updated", _usuarioService.GetTag(), summary);
            };

            Triggers<Usuario>.Deleted += async entry =>
            {
                await Clients.All.SendAsync("deleted", _usuarioService.GetTag(), entry.Entity.Id);
            };
        }
    }
}

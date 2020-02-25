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
        static UsuarioNotifier()
        {
            Triggers<Usuario>.GlobalInserted.Add<(IUsuarioService, IHubContext<HubNotificacoes>)>(async insertingEntry =>
            {
                var summary = await insertingEntry.Service.Item1.GetSummaryAsync(insertingEntry.Entity);
                await insertingEntry.Service.Item2.Clients.All.SendAsync("inserted", insertingEntry.Service.Item1.GetTag(), summary);
            });

            Triggers<Usuario>.GlobalUpdated.Add<(IUsuarioService, IHubContext<HubNotificacoes>)>(async updatingEntry =>
            {
                var summary = await updatingEntry.Service.Item1.GetSummaryAsync(updatingEntry.Entity);
                await updatingEntry.Service.Item2.Clients.All.SendAsync("updated", updatingEntry.Service.Item1.GetTag(), summary);
            });

            Triggers<Usuario>.GlobalDeleted.Add<(IUsuarioService, IHubContext<HubNotificacoes>)>(async deletedEntry =>
            {
                await deletedEntry.Service.Item2.Clients.All.SendAsync("deleted", deletedEntry.Service.Item1.GetTag(), deletedEntry.Entity.Id);
            });
        }
    }
}

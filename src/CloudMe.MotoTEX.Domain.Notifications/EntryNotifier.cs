using CloudMe.MotoTEX.Domain.Notifications.Hubs;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CloudMe.MotoTEX.Domain.Notifications
{
    public class EntityNotifier<TEntryService, TEntry, TEntrySummary, TEntryKey>
        where TEntry: EntryBase<TEntryKey>
        where TEntryService : IServiceBase<TEntry, TEntrySummary, TEntryKey>
    {
        private static bool eventsRegistered = false;

        static EntityNotifier()
        {
            if (!eventsRegistered)
            {
                Triggers<TEntry>.GlobalInserted.Add<(TEntryService, IHubContext<HubNotificacoes>)>(async insertingEntry =>
                {
                    var summary = await insertingEntry.Service.Item1.GetSummaryAsync(insertingEntry.Entity);
                    await insertingEntry.Service.Item2.Clients.All.SendAsync("inserted", insertingEntry.Service.Item1.GetTag(), summary);
                });

                Triggers<TEntry>.GlobalUpdated.Add<(TEntryService, IHubContext<HubNotificacoes>)>(async updatingEntry =>
                {
                    var summary = await updatingEntry.Service.Item1.GetSummaryAsync(updatingEntry.Entity);
                    await updatingEntry.Service.Item2.Clients.All.SendAsync("updated", updatingEntry.Service.Item1.GetTag(), summary);
                });

                Triggers<TEntry>.GlobalDeleting.Add<(TEntryService, IHubContext<HubNotificacoes>)>(async deletingEntry =>
                {
                    await deletingEntry.Service.Item2.Clients.All.SendAsync("deleted", deletingEntry.Service.Item1.GetTag(), deletingEntry.Entity.Id);
                });

                eventsRegistered = true;
            }
        }
    }
}

using CloudMe.MotoTEX.Domain.Notifications.Hubs;
using CloudMe.MotoTEX.Domain.Services.Abstracts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace CloudMe.MotoTEX.Domain.Notifications.Compat
{
    [Authorize]
    public class EntityNotifier<TEntryService, TEntry, TEntrySummary, TEntryKey> : Hub
        where TEntry : EntryBase<TEntryKey>
        where TEntryService : IServiceBase<TEntry, TEntrySummary, TEntryKey>
    {
        TEntryService _entryService;
        IHubContext<EntityNotifier<TEntryService, TEntry, TEntrySummary, TEntryKey>> _hubContext = null;
        IHubContext<HubNotificacoes> hubNotificacoes = null;
        private static bool eventsRegistered = false;

        public EntityNotifier(
            TEntryService entryService, 
            IHubContext<EntityNotifier<TEntryService, TEntry, TEntrySummary, TEntryKey>> hubContext,
            IHubContext<HubNotificacoes> hubNotificacoes)
        {
            _entryService = entryService;
            _hubContext = hubContext;
            this.hubNotificacoes = hubNotificacoes;

            if (!eventsRegistered)
            {
                Triggers<TEntry>.Inserted += async insertingEntry =>
                {
                    var summary = await _entryService.GetSummaryAsync(insertingEntry.Entity);
                    await _hubContext.Clients.All.SendAsync("inserted", _entryService.GetTag(), summary); // COMPAT
                    await hubNotificacoes.Clients.All.SendAsync("inserted", _entryService.GetTag(), summary);
                };

                Triggers<TEntry>.Updated += async updatingEntry =>
                {
                    var summary = await _entryService.GetSummaryAsync(updatingEntry.Entity);
                    await _hubContext.Clients.All.SendAsync("updated", _entryService.GetTag(), summary); // COMPAT
                    await hubNotificacoes.Clients.All.SendAsync("updated", _entryService.GetTag(), summary);
                };

                Triggers<TEntry>.Deleting += async deletingEntry =>
                {
                    await _hubContext.Clients.All.SendAsync("deleted", _entryService.GetTag(), deletingEntry.Entity.Id); // COMPAT
                    await hubNotificacoes.Clients.All.SendAsync("deleted", _entryService.GetTag(), deletingEntry.Entity.Id);
                };

                eventsRegistered = true;
            }
        }
    }
}

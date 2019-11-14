using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CloudMe.ToDeTaxi.Domain.Notifications
{
    //[Authorize]
    public class EntityNotifier<TEntryService, TEntry, TEntrySummary, TEntryKey> : BaseNotifier
        where TEntry: EntryBase<TEntryKey>
        where TEntryService : IServiceBase<TEntry, TEntrySummary, TEntryKey>
    {
        TEntryService _entryService;
        IHubContext<EntityNotifier<TEntryService, TEntry, TEntrySummary, TEntryKey>> _hubContext = null;

        public EntityNotifier(TEntryService entryService, IHubContext<EntityNotifier<TEntryService, TEntry, TEntrySummary, TEntryKey>> hubContext)
        {
            _entryService = entryService;
            _hubContext = hubContext;

            //Triggers<TEntry>.Inserted += async entry =>
            Triggers<TEntry>.Inserting += async entry =>
            {
                var summary = await _entryService.GetSummaryAsync(entry.Entity);
                await _hubContext.Clients.All.SendAsync("inserted", _entryService.GetTag(), summary);
            };

            //Triggers<TEntry>.Updated += async entry =>
            Triggers<TEntry>.Updating += async entry =>
            {
                var summary = await _entryService.GetSummaryAsync(entry.Entity);
                await _hubContext.Clients.All.SendAsync("updated", _entryService.GetTag(), summary);
            };

            //Triggers<TEntry>.Deleted += async entry =>
            Triggers<TEntry>.Deleting += async entry =>
            {
                await _hubContext.Clients.All.SendAsync("deleted", _entryService.GetTag(), entry.Entity.Id);
            };
        }
    }
}

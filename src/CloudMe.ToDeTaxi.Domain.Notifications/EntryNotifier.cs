using CloudMe.ToDeTaxi.Domain.Services.Abstracts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using EntityFrameworkCore.Triggers;
using Microsoft.AspNetCore.SignalR;

namespace CloudMe.ToDeTaxi.Domain.Notifications
{
    public class UsuarioNotifier<TEntryService, TEntry, TEntrySummary, TEntryKey> : BaseNotifier
        where TEntry: EntryBase<TEntryKey>
        where TEntryService : IServiceBase<TEntry, TEntrySummary, TEntryKey>
    {
        TEntryService _entryService;
        UsuarioNotifier(TEntryService entryService)
        {
            _entryService = entryService;

            Triggers<TEntry>.Inserted += async entry =>
            {
                var summary = await _entryService.GetSummaryAsync(entry.Entity);
                await Clients.All.SendAsync("inserted", _entryService.GetTag(), summary);
            };

            Triggers<TEntry>.Updated += async entry =>
            {
                var summary = await _entryService.GetSummaryAsync(entry.Entity);
                await Clients.All.SendAsync("updated", _entryService.GetTag(), summary);
            };

            Triggers<TEntry>.Deleted += async entry =>
            {
                await Clients.All.SendAsync("deleted", _entryService.GetTag(), entry.Entity.Id);
            };
        }
    }
}

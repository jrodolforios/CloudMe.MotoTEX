using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Domain.Model;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    // Basic CRUD and accessories
    public interface IServiceBase<TEntry, TEntrySummary, TEntryKey> : INotifiable
    {
        Task<int> GetCount();

        Task<TEntry> Get(TEntryKey key, string[] paths = null);
        Task<IEnumerable<TEntry>> GetAll(string[] paths = null);
        IEnumerable<TEntry> Search(Expression<Func<TEntry, bool>> where, string[] paths = null, SearchOptions options = null);

        Task<TEntrySummary> GetSummaryAsync(TEntryKey key);
        Task<TEntrySummary> GetSummaryAsync(TEntry entry);
        Task<IEnumerable<TEntrySummary>> GetAllSummariesAsync();
        Task<IEnumerable<TEntrySummary>> GetAllSummariesAsync(IEnumerable<TEntry> entries);

        Task<TEntry> CreateAsync(TEntrySummary summary);
        Task<TEntry> UpdateAsync(TEntrySummary summary);
        Task<bool> DeleteAsync(TEntryKey key);
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using CloudMe.ToDeTaxi.Domain.Model;

namespace CloudMe.ToDeTaxi.Domain.Services
{
    public abstract class ServiceBase<TEntry, TEntrySummary, TEntryKey> 
        : Notifiable, Abstracts.IServiceBase<TEntry, TEntrySummary, TEntryKey> where TEntry : class
    {
        protected abstract IBaseRepository<TEntry> GetRepository();
        protected abstract void ValidateSummary(TEntrySummary summary);
        protected abstract Task<TEntrySummary> CreateSummaryAsync(TEntry entry);
        protected abstract Task<TEntry> CreateEntryAsync(TEntrySummary summary);
        protected abstract void UpdateEntry(TEntry entry, TEntrySummary summary);
        protected abstract TEntryKey GetKeyFromSummary(TEntrySummary summary);


        public async Task<int> GetCount()
        {
            return await GetRepository().CountAsync();
        }

        public async Task<TEntry> Get(TEntryKey key, string[] paths = null)
        {
            return await GetRepository().FindByIdAsync(key, paths);
        }

        public virtual async Task<IEnumerable<TEntry>> GetAll(string[] paths = null)
        {
            return await GetRepository().FindAllAsync(paths);
        }

        public IEnumerable<TEntry> Search(Expression<Func<TEntry, bool>> where, string[] paths = null, SearchOptions options = null)
        {
            var rawItens = GetRepository().Search(where, paths);
            if(options != null)
            {
                rawItens = rawItens
                    .Skip(options.itensPerPage * options.page)
                    .Take(options.itensPerPage);
            }

            return rawItens;
        }


        public async Task<TEntrySummary> GetSummaryAsync(TEntryKey key)
        {
            var entry = await GetRepository().FindByIdAsync(key);
            return await GetSummaryAsync(entry);
        }

        public async Task<TEntrySummary> GetSummaryAsync(TEntry entry)
        {
            return await CreateSummaryAsync(entry);
        }

        public async Task<IEnumerable<TEntrySummary>> GetAllSummariesAsync()
        {
            var entries = await this.GetAll(null);
            return await GetAllSummariesAsync(entries);
        }

        public async Task<IEnumerable<TEntrySummary>> GetAllSummariesAsync(IEnumerable<TEntry> entries)
        {
            List<TEntrySummary> summaries = new List<TEntrySummary>();
            foreach (var entry in entries)
            {
                summaries.Add(await CreateSummaryAsync(entry));
            }

            return summaries;
        }

        public virtual async Task<TEntry> CreateAsync(TEntrySummary summary)
        {
            ValidateSummary(summary);

            if (IsInvalid())
            {
                return null;
            }

            var entry = await CreateEntryAsync(summary);

            if (IsInvalid())
            {
                return null;
            }

            if (await GetRepository().SaveAsync(entry))
            {
                return entry;
            }
            else
            {
                this.AddNotifications(GetRepository().Notifications);
            }

            return null;
        }

        public async Task<TEntry> UpdateAsync(TEntrySummary summary)
        {
            ValidateSummary(summary);

            if (IsInvalid())
            {
                return null;
            }

            var entry = await GetRepository().FindByIdAsync(GetKeyFromSummary(summary));
            if(entry != null)
            {
                UpdateEntry(entry, summary);

                if (IsInvalid())
                {
                    return null;
                }

                if (await GetRepository().ModifyAsync(entry))
                {
                    return entry;
                }
            }

            return null;
        }

        public virtual async Task<bool> DeleteAsync(TEntryKey key)
        {
            var repo = GetRepository();
            var entry = await repo.FindByIdAsync(key);
            return await repo.DeleteAsync(entry);
        }

    }
}

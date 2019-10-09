using prmToolkit.NotificationPattern;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories
{
    public interface IBaseRepository<TEntry>: INotifiable where TEntry : class
    {
        Task<int> CountAsync();
        Task<bool> DeleteAsync(TEntry entry, bool logical = true);
        void Detach<TEntity>(TEntity entry) where TEntity : class;
        IEnumerable<TEntry> FindAll();
        Task<IEnumerable<TEntry>> FindAllAsync();
        Task<IEnumerable<TEntry>> FindAllAsync(string[] paths);
        Task<TEntry> FindByIdAsync(object key);
        Task<TEntry> FindByIdAsync(params object[] keyComposition);
        Task<TEntry> FindByIdAsync(object key, string[] paths);
        Task<bool> ModifyAsync(TEntry entry);
        Task<bool> SaveAsync(TEntry entry);
        IEnumerable<TEntry> Search(Expression<Func<TEntry, bool>> where, string[] paths = null);
    }
}
using prmToolkit.NotificationPattern;
using System.Threading;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions
{
    public interface IUnitOfWork : INotifiable
    {
        Task<bool> CommitAsync(CancellationToken cancellationToken = default);
    }
}

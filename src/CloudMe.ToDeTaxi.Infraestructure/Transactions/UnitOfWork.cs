using Microsoft.EntityFrameworkCore;
using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Infraestructure.Abstracts.Transactions
{
    public class UnitOfWork : Notifiable, IUnitOfWork
    {
        private readonly CloudMeToDeTaxiContext _context;

        public UnitOfWork(CloudMeToDeTaxiContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            return await this.SaveChangesAsync() >= 0;
        }

        private async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                int result = -1;
                try
                {
                    result = await _context.SaveChangesAsync(cancellationToken);
                }
                catch (DbUpdateException dbEx)
                {
                    Exception raise = dbEx.InnerException;
                    string message = string.Format("{0}:{1}", raise.Data["Code"], raise.Data["MessageText"]);
                    raise = new InvalidOperationException(message, raise);
                    AddNotification("DbUpdateException", message);
                    result = -1;
                }
                catch (Exception dbEx)
                {
                    Exception raise = dbEx.InnerException;
                    string message = raise.Message;
                    AddNotification("DbUpdateException", message);
                    result = -1;
                }
                finally
                {
                    if (result > 0)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                return result;
            }
        }
    }
}

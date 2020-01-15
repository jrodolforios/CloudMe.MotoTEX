using Microsoft.EntityFrameworkCore;
using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Infraestructure.Abstracts.Transactions
{
    public class UnitOfWork : Notifiable, IUnitOfWork
    {
        private readonly CloudMeMotoTEXContext _context;

        public UnitOfWork(CloudMeMotoTEXContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
        {
            bool result = false;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    if((await _context.SaveChangesAsync(cancellationToken)) >= 0)
                    {
                        transaction.Commit();
                        result = true;
                    }
                    else
                    {
                        transaction.Rollback();
                        result = false;
                    }
                }
            }
            catch (Exception dbEx)
            {
                Exception raise = dbEx.InnerException;
                string message = raise.Message;
                AddNotification("DatabaseException", message);
                result = false;
            }

            return result;
        }
    }
}

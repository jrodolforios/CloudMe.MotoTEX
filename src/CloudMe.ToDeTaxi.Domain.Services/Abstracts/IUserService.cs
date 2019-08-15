using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Infraestructure.Entries;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface IUserService : INotifiable
    {
        IEnumerable<User> FindAll();
        IEnumerable<User> FindAllbyCompany(Guid idCompany);
        Task<User> FindByIdAsync(Guid id);
    }
}
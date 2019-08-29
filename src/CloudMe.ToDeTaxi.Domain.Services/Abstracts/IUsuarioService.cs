using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Infraestructure.Entries;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface IUsuarioService : INotifiable
    {
        IEnumerable<Usuario> FindAll();
        IEnumerable<Usuario> FindAllbyCompany(Guid idCompany);
        Task<Usuario> FindByIdAsync(Guid id);
    }
}
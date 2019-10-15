using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using prmToolkit.NotificationPattern;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Usuario;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface IUsuarioService : IServiceBase<Usuario, UsuarioSummary, Guid>
    {
        Task<bool> BloquearAsync(Guid key, bool bloquear);
        Task<bool> ChangeLoginAsync(Guid key, string new_login);
        Task<bool> ChangePasswordAsync(Guid key, string old_password, string new_password);
    }
}
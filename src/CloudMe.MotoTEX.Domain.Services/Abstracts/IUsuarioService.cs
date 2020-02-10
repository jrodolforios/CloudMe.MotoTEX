using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using prmToolkit.NotificationPattern;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Usuario;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface IUsuarioService : IServiceBase<Usuario, UsuarioSummary, Guid>
    {
        Task<bool> BloquearAsync(Guid key, bool bloquear);
        Task<bool> ChangeLoginAsync(Guid key, string new_login);
        Task<bool> ChangePasswordAsync(Guid key, string old_password, string new_password);
        Task<bool> CheckLogin(string login);
        Task<UsuarioSummary> GetSummaryByNameAsync(string name);
        Task<bool> CheckEmail(string email);
        Task<bool> InformarDeviceToken(Guid id_usuario, string token);
    }
}
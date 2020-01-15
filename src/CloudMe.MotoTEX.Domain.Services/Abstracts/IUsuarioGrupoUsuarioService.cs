using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Usuario;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface IUsuarioGrupoUsuarioService : IServiceBase<UsuarioGrupoUsuario, UsuarioGrupoUsuarioSummary, Guid>
    {
    }
}

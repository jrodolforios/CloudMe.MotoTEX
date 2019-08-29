using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Usuario;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface IGrupoUsuarioService : IServiceBase<GrupoUsuario, GrupoUsuarioSummary, Guid>
    {
    }
}

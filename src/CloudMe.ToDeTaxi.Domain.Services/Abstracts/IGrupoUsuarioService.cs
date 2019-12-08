using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Usuario;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface IGrupoUsuarioService : IServiceBase<GrupoUsuario, GrupoUsuarioSummary, Guid>
    {
        Task<IEnumerable<GrupoUsuarioSummary>> GetAllSummariesByUserAsync(Guid user_id);
    }
}

using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Usuario;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface IGrupoUsuarioService : IServiceBase<GrupoUsuario, GrupoUsuarioSummary, Guid>
    {
        Task<IEnumerable<GrupoUsuarioSummary>> GetAllSummariesByUserAsync(Guid user_id);
        Task<GrupoUsuarioSummary> GetSummaryByNameAsync(string name);
    }
}

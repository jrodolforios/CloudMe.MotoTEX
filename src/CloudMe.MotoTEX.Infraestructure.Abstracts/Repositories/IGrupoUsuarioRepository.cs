using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudMe.MotoTEX.Infraestructure.Entries;

namespace CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories
{
    public interface IGrupoUsuarioRepository : IBaseRepository<GrupoUsuario>
    {
        Task<IEnumerable<GrupoUsuario>> GetAllByUserId(Guid user_id);
    }
}

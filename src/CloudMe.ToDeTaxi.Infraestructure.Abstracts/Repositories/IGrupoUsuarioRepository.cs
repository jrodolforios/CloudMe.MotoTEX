using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudMe.ToDeTaxi.Infraestructure.Entries;

namespace CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories
{
    public interface IGrupoUsuarioRepository : IBaseRepository<GrupoUsuario>
    {
        Task<IEnumerable<GrupoUsuario>> GetAllByUserId(Guid user_id);
    }
}

using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CloudMe.MotoTEX.Infraestructure.Repositories
{
    public class GrupoUsuarioRepository : BaseRepository<GrupoUsuario>, IGrupoUsuarioRepository
    {
        public GrupoUsuarioRepository(CloudMeMotoTEXContext context) : base(context)
        {

        }

        public async Task<IEnumerable<GrupoUsuario>> GetAllByUserId(Guid user_id)
        {
            return await Task.Run(() =>
            {
                return
                    from usrGrpUsr in Context.UsuariosGruposUsuarios
                    .Include(x => x.GrupoUsuario)
                    .Where(x => x.IdUsuario == user_id)
                    select usrGrpUsr.GrupoUsuario;
            });
        }
    }
}

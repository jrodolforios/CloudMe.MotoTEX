using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class GrupoUsuarioRepository : BaseRepository<GrupoUsuario>, IGrupoUsuarioRepository
    {
        public GrupoUsuarioRepository(CloudMeToDeTaxiContext context) : base(context)
        {

        }

        public async Task<IEnumerable<GrupoUsuario>> GetAllByUserId(Guid user_id)
        {
            var usrGrpUsrs =
                from usrGrpUsr in Context.Set<UsuarioGrupoUsuario>()
                .Include(x => x.GrupoUsuario)
                .Where(x => x.IdUsuario == user_id)
                select usrGrpUsr.GrupoUsuario;

            return usrGrpUsrs.AsEnumerable();
        }
    }
}

using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;

namespace CloudMe.MotoTEX.Infraestructure.Repositories
{
    public class UsuarioGrupoUsuarioRepository : BaseRepository<UsuarioGrupoUsuario>, IUsuarioGrupoUsuarioRepository
    {
        public UsuarioGrupoUsuarioRepository(CloudMeMotoTEXContext context) : base(context)
        {
        }
    }
}

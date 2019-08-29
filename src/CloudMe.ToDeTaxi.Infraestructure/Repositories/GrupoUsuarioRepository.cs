using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class GrupoUsuarioRepository : BaseRepository<GrupoUsuario>, IGrupoUsuarioRepository
    {
        public GrupoUsuarioRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }
    }
}

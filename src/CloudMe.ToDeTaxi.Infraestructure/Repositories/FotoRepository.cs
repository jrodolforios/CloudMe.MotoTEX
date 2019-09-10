using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class FotoRepository : BaseRepository<Foto>, IFotoRepository
    {
        public FotoRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }
    }
}

using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class CorridaRepository : BaseRepository<Corrida>, ICorridaRepository
    {
        public CorridaRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }
    }
}

using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;

namespace CloudMe.MotoTEX.Infraestructure.Repositories
{
    public class CorridaRepository : BaseRepository<Corrida>, ICorridaRepository
    {
        public CorridaRepository(CloudMeMotoTEXContext context) : base(context)
        {
        }
    }
}

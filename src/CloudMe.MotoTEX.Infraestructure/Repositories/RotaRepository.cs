using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;

namespace CloudMe.MotoTEX.Infraestructure.Repositories
{
    public class RotaRepository : BaseRepository<Rota>, IRotaRepository
    {
        public RotaRepository(CloudMeMotoTEXContext context) : base(context)
        {
        }
    }
}

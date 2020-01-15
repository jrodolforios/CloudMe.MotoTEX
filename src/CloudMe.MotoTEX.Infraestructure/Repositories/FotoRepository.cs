using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;

namespace CloudMe.MotoTEX.Infraestructure.Repositories
{
    public class FotoRepository : BaseRepository<Foto>, IFotoRepository
    {
        public FotoRepository(CloudMeMotoTEXContext context) : base(context)
        {
        }
    }
}

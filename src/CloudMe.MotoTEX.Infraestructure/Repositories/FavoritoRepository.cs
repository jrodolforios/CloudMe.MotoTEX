using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;

namespace CloudMe.MotoTEX.Infraestructure.Repositories
{
    public class FavoritoRepository : BaseRepository<Favorito>, IFavoritoRepository
    {
        public FavoritoRepository(CloudMeMotoTEXContext context) : base(context)
        {
        }
    }
}

using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class FavoritoRepository : BaseRepository<Favorito>, IFavoritoRepository
    {
        public FavoritoRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }
    }
}

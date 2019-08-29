using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class RotaRepository : BaseRepository<Rota>, IRotaRepository
    {
        public RotaRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }
    }
}

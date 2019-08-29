using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class TarifaRepository : BaseRepository<Tarifa>, ITarifaRepository
    {
        public TarifaRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }
    }
}

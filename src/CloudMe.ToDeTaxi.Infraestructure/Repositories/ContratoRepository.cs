using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class ContratoRepository : BaseRepository<Contrato>, IContratoRepository
    {
        public ContratoRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }
    }
}

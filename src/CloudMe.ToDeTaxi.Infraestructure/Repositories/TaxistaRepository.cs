using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class TaxistaRepository : BaseRepository<Taxista>, ITaxistaRepository
    {
        public TaxistaRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }
    }
}

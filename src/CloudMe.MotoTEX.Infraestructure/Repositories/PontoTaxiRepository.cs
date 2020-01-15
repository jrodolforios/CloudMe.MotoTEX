using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;

namespace CloudMe.MotoTEX.Infraestructure.Repositories
{
    public class PontoTaxiRepository : BaseRepository<PontoTaxi>, IPontoTaxiRepository
    {
        public PontoTaxiRepository(CloudMeMotoTEXContext context) : base(context)
        {
        }
    }
}

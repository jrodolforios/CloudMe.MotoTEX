using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;

namespace CloudMe.MotoTEX.Infraestructure.Repositories
{
    public class TaxistaRepository : BaseRepository<Taxista>, ITaxistaRepository
    {
        public TaxistaRepository(CloudMeMotoTEXContext context) : base(context)
        {
        }
    }
}

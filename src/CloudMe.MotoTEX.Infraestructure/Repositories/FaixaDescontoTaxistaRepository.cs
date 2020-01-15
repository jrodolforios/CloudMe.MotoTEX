using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;

namespace CloudMe.MotoTEX.Infraestructure.Repositories
{
    public class FaixaDescontoTaxistaRepository : BaseRepository<FaixaDescontoTaxista>, IFaixaDescontoTaxistaRepository
    {
        public FaixaDescontoTaxistaRepository(CloudMeMotoTEXContext context) : base(context)
        {
        }
    }
}

using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class FaixaDescontoTaxistaRepository : BaseRepository<FaixaDescontoTaxista>, IFaixaDescontoTaxistaRepository
    {
        public FaixaDescontoTaxistaRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }
    }
}

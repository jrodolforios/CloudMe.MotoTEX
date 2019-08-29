using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class FaixaDescontoRepository : BaseRepository<FaixaDesconto>, IFaixaDescontoRepository
    {
        public FaixaDescontoRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }
    }
}

using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;

namespace CloudMe.MotoTEX.Infraestructure.Repositories
{
    public class FaixaDescontoRepository : BaseRepository<FaixaDesconto>, IFaixaDescontoRepository
    {
        public FaixaDescontoRepository(CloudMeMotoTEXContext context) : base(context)
        {
        }
    }
}

using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;

namespace CloudMe.MotoTEX.Infraestructure.Repositories
{
    public class FaturamentoRepository : BaseRepository<Faturamento>, IFaturamentoRepository
    {
        public FaturamentoRepository(CloudMeMotoTEXContext context) : base(context)
        {
        }
    }
}

using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;

namespace CloudMe.MotoTEX.Infraestructure.Repositories
{
    public class FormaPagamentoTaxistaRepository : BaseRepository<FormaPagamentoTaxista>, IFormaPagamentoTaxistaRepository
    {
        public FormaPagamentoTaxistaRepository(CloudMeMotoTEXContext context) : base(context)
        {
        }
    }
}

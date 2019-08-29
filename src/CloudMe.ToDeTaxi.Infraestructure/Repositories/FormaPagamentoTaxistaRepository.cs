using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class FormaPagamentoTaxistaRepository : BaseRepository<FormaPagamentoTaxista>, IFormaPagamentoTaxistaRepository
    {
        public FormaPagamentoTaxistaRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }
    }
}

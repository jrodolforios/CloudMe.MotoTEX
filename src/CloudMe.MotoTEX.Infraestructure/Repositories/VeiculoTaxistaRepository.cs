using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;

namespace CloudMe.MotoTEX.Infraestructure.Repositories
{
    public class VeiculoTaxistaRepository : BaseRepository<VeiculoTaxista>, IVeiculoTaxistaRepository
    {
        public VeiculoTaxistaRepository(CloudMeMotoTEXContext context) : base(context)
        {
        }
    }
}

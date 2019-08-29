using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class VeiculoTaxistaRepository : BaseRepository<VeiculoTaxista>, IVeiculoTaxistaRepository
    {
        public VeiculoTaxistaRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }
    }
}

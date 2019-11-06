using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class CorVeiculoRepository : BaseRepository<CorVeiculo>, ICorVeiculoRepository
    {
        public CorVeiculoRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }
    }
}

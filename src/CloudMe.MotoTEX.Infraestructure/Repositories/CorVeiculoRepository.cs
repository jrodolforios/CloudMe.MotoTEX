using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;

namespace CloudMe.MotoTEX.Infraestructure.Repositories
{
    public class CorVeiculoRepository : BaseRepository<CorVeiculo>, ICorVeiculoRepository
    {
        public CorVeiculoRepository(CloudMeMotoTEXContext context) : base(context)
        {
        }
    }
}

using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;

namespace CloudMe.MotoTEX.Infraestructure.Repositories
{
    public class EmergenciaRepository : BaseRepository<Emergencia>, IEmergenciaRepository
    {
        public EmergenciaRepository(CloudMeMotoTEXContext context) : base(context)
        {
        }
    }
}

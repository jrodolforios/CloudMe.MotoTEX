using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class EmergenciaRepository : BaseRepository<Emergencia>, IEmergenciaRepository
    {
        public EmergenciaRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }
    }
}

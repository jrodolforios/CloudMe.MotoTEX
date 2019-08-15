using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class CallRepository : BaseRepository<Call>, ICallRepository
    {
        public CallRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }
    }
}

using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class PassageiroRepository : BaseRepository<Passageiro>, IPassageiroRepository
    {
        public PassageiroRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }
    }
}

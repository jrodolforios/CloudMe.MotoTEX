using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;

namespace CloudMe.MotoTEX.Infraestructure.Repositories
{
    public class PassageiroRepository : BaseRepository<Passageiro>, IPassageiroRepository
    {
        public PassageiroRepository(CloudMeMotoTEXContext context) : base(context)
        {
        }
    }
}

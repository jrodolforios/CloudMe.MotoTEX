using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class MensagemRepository : BaseRepository<Mensagem>, IMensagemRepository
    {
        public MensagemRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }
    }
}

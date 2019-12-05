using CloudMe.ToDeTaxi.Infraestructure.EF.Contexts;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Infraestructure.Abstracts.Repositories;

namespace CloudMe.ToDeTaxi.Infraestructure.Repositories
{
    public class MensagemDestinatarioRepository : BaseRepository<MensagemDestinatario>, IMensagemDestinatarioRepository
    {
        public MensagemDestinatarioRepository(CloudMeToDeTaxiContext context) : base(context)
        {
        }
    }
}

using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;

namespace CloudMe.MotoTEX.Infraestructure.Repositories
{
    public class MensagemDestinatarioRepository : BaseRepository<MensagemDestinatario>, IMensagemDestinatarioRepository
    {
        public MensagemDestinatarioRepository(CloudMeMotoTEXContext context) : base(context)
        {
        }
    }
}

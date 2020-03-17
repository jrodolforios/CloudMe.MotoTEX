using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries.Piloto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Infraestructure.Repositories
{
    public class HabilitacaoRepository : BaseRepository<Habilitacao>, IHabilitacaoRepository
    {
        public HabilitacaoRepository(CloudMeMotoTEXContext context) : base(context)
        {
        }
    }
}

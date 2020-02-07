using CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories;
using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Infraestructure.Entries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Infraestructure.Repositories
{
    public class FaixaAtivacaoRepository : BaseRepository<FaixaAtivacao>, IFaixaAtivacaoRepository
    {
        public FaixaAtivacaoRepository(CloudMeMotoTEXContext context) : base(context)
        {
        }

        public async Task<IEnumerable<FaixaAtivacao>> GetAllByRadius()
        {
            return await Task.FromResult(Context.FaixasAtivacao.OrderBy(x => x.Raio).AsEnumerable());
        }

    }
}

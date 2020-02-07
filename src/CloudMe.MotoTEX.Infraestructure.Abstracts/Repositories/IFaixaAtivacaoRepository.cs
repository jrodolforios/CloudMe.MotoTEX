using CloudMe.MotoTEX.Infraestructure.Entries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Infraestructure.Abstracts.Repositories
{
    public interface IFaixaAtivacaoRepository : IBaseRepository<FaixaAtivacao>
    {
        Task<IEnumerable<FaixaAtivacao>> GetAllByRadius();
    }
}

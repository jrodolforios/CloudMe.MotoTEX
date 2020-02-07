using CloudMe.MotoTEX.Domain.Model.Corrida;
using CloudMe.MotoTEX.Infraestructure.Entries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface IFaixaAtivacaoService : IServiceBase<FaixaAtivacao, FaixaAtivacaoSummary, Guid>
    {
        Task<IEnumerable<FaixaAtivacao>> GetAllByRadius();
    }
}

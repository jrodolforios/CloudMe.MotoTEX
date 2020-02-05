using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Localizacao;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface ILocalizacaoService : IServiceBase<Localizacao, LocalizacaoSummary, Guid>
    {
        Task<int> GetQtTaxistasOnline();
    }
}

using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Taxista;
using System.Threading.Tasks;
using CloudMe.MotoTEX.Domain.Model.Localizacao;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface ITaxistaService : IServiceBase<Taxista, TaxistaSummary, Guid>
    {
        /*Task<bool> AssociarFoto(Guid Key, Guid idFoto);
        Task<bool> Ativar(Guid Key, bool ativar);*/
        Task<TaxistaSummary> GetByUserId(Guid id);
        Task<bool> MakeTaxistOnlineAsync(Guid id, bool disponivel);
        Task<bool> InformarLocalizacao(Guid Key, LocalizacaoSummary localizacao);
        Task<int> ClassificacaoTaxista(Guid id);
    }
}

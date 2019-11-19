using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Taxista;
using System.Threading.Tasks;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
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

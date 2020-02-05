using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Taxista;
using System.Threading.Tasks;
using CloudMe.MotoTEX.Domain.Model.Localizacao;
using System.Collections.Generic;

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

        Task<IEnumerable<Taxista>> ProcurarPorDistancia(LocalizacaoSummary origem, double? raio_minimo, double? raio_maximo, string [] paths = null);
    }
}

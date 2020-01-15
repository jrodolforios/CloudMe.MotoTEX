using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Passageiro;
using System.Threading.Tasks;
using CloudMe.MotoTEX.Domain.Model.Localizacao;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface IPassageiroService : IServiceBase<Passageiro, PassageiroSummary, Guid>
    {
        //Task<bool> AssociarFoto(Guid Key, Guid idFoto);
        Task<PassageiroSummary> GetByUserId(Guid Key);
        Task<bool> InformarLocalizacao(Guid Key, LocalizacaoSummary localizacao);
        Task<int> ClassificacaoPassageiro(Guid id);
    }
}

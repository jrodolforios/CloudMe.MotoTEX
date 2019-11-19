using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Passageiro;
using System.Threading.Tasks;
using CloudMe.ToDeTaxi.Domain.Model.Localizacao;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface IPassageiroService : IServiceBase<Passageiro, PassageiroSummary, Guid>
    {
        //Task<bool> AssociarFoto(Guid Key, Guid idFoto);
        Task<PassageiroSummary> GetByUserId(Guid Key);
        Task<bool> InformarLocalizacao(Guid Key, LocalizacaoSummary localizacao);
        Task<int> ClassificacaoPassageiro(Guid id);
    }
}

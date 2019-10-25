using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Passageiro;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface IPassageiroService : IServiceBase<Passageiro, PassageiroSummary, Guid>
    {
        //Task<bool> AssociarFoto(Guid Key, Guid idFoto);
        Task<PassageiroSummary> GetByUserId(Guid Key);
    }
}

using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Passageiro;
using CloudMe.ToDeTaxi.Domain.Model;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface IContratoService : IServiceBase<Contrato, ContratoSummary, Guid>
    {
        Task<ContratoSummary> UltimoContratoValido();
    }
}

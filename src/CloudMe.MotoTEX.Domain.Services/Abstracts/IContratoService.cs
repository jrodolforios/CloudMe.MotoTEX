using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Passageiro;
using CloudMe.MotoTEX.Domain.Model;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface IContratoService : IServiceBase<Contrato, ContratoSummary, Guid>
    {
        Task<ContratoSummary> UltimoContratoValido();
    }
}

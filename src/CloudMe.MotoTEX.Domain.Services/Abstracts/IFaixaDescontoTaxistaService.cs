using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Taxista;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface IFaixaDescontoTaxistaService : IServiceBase<FaixaDescontoTaxista, FaixaDescontoTaxistaSummary, Guid>
    {
        Task<IEnumerable<FaixaDescontoTaxistaSummary>> GetByTaxistId(Guid id);
        Task<bool> DeleteByTaxistId(Guid id);
    }
}

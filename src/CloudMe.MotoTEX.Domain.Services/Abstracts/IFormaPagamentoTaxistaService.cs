using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Taxista;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface IFormaPagamentoTaxistaService : IServiceBase<FormaPagamentoTaxista, FormaPagamentoTaxistaSummary, Guid>
    {
        Task<IEnumerable<FormaPagamentoTaxistaSummary>> GetByTaxistId(Guid id);
        Task<bool> DeleteByTaxistId(Guid id);
    }
}

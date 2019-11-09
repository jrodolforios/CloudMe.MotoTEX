using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Taxista;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface IFormaPagamentoTaxistaService : IServiceBase<FormaPagamentoTaxista, FormaPagamentoTaxistaSummary, Guid>
    {
        Task<List<FormaPagamentoTaxistaSummary>> GetByTaxistId(Guid id);
        Task<bool> DeleteByTaxistId(Guid id);
    }
}

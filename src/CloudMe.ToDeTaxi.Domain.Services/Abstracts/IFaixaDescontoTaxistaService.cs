using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Taxista;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface IFaixaDescontoTaxistaService : IServiceBase<FaixaDescontoTaxista, FaixaDescontoTaxistaSummary, Guid>
    {
        Task<List<FaixaDescontoTaxistaSummary>> GetByTaxistId(Guid id);
        Task<bool> DeleteByTaxistId(Guid id);
    }
}

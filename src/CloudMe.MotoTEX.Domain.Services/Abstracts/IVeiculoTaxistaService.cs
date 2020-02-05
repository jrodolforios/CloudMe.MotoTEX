using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Taxista;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface IVeiculoTaxistaService : IServiceBase<VeiculoTaxista, VeiculoTaxistaSummary, Guid>
    {
        Task<bool> IsTaxiAtivoEmUsoPorOutroTaxista(Guid id);
        Task<IEnumerable<VeiculoTaxistaSummary>> ConsultaVeiculosDeTaxista(Guid id);
    }
}

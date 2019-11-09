using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Taxista;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface IVeiculoTaxistaService : IServiceBase<VeiculoTaxista, VeiculoTaxistaSummary, Guid>
    {
        bool IsTaxiAtivoEmUsoPorOutroTaxista(Guid id);
        Task<List<VeiculoTaxistaSummary>> ConsultaVeiculosDeTaxista(Guid id);
    }
}

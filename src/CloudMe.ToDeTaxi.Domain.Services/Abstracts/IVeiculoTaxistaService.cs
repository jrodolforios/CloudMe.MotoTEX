using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Taxista;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface IVeiculoTaxistaService : IServiceBase<VeiculoTaxista, VeiculoTaxistaSummary, Guid>
    {
    }
}

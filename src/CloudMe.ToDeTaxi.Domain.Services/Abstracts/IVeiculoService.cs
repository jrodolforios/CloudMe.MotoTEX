using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Veiculo;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface IVeiculoService : IServiceBase<Veiculo, VeiculoSummary, Guid>
    {
    }
}

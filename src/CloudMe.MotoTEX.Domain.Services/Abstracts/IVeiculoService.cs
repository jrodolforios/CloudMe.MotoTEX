using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Veiculo;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface IVeiculoService : IServiceBase<Veiculo, VeiculoSummary, Guid>
    {
    }
}

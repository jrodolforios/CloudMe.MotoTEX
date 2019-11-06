using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Veiculo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface ICorVeiculoService : IServiceBase<CorVeiculo, CorVeiculoSummary, Guid>
    {
    }
}

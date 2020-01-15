using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Veiculo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface ICorVeiculoService : IServiceBase<CorVeiculo, CorVeiculoSummary, Guid>
    {
    }
}

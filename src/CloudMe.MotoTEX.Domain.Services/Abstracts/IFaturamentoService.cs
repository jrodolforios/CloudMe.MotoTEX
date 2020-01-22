using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Veiculo;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudMe.MotoTEX.Domain.Model.Faturamento;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface IFaturamentoService : IServiceBase<Faturamento, FaturamentoSummary, Guid>
    {
    }
}

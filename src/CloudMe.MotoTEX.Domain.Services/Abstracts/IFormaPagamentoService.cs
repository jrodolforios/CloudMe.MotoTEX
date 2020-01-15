using System;
using CloudMe.MotoTEX.Infraestructure.Entries;
using CloudMe.MotoTEX.Domain.Model.Corrida;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface IFormaPagamentoService : IServiceBase<FormaPagamento, FormaPagamentoSummary, Guid>
    {
    }
}

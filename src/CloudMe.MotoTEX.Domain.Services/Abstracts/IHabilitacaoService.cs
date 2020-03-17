using CloudMe.MotoTEX.Domain.Model.Taxista;
using CloudMe.MotoTEX.Infraestructure.Entries.Piloto;
using System;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface IHabilitacaoService : IServiceBase<Habilitacao, HabilitacaoSummary, Guid>
    {
    }
}

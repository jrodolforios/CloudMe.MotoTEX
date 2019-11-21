using System;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using CloudMe.ToDeTaxi.Domain.Model.Corrida;
using System.Collections.Generic;
using System.Threading.Tasks;
using CloudMe.ToDeTaxi.Domain.Enums;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface ISolicitacaoCorridaService : IServiceBase<SolicitacaoCorrida, SolicitacaoCorridaSummary, Guid>
    {
        Task<bool> RegistrarAcaoTaxista(Guid id_solicitacao, Guid id_taxista, AcaoTaxistaSolicitacaoCorrida acao);
    }
}

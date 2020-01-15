using CloudMe.MotoTEX.Domain.Enums;
using CloudMe.MotoTEX.Domain.Model.Taxista;
using CloudMe.MotoTEX.Infraestructure.Entries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Services.Abstracts
{
    public interface IEmergenciaService : IServiceBase<Emergencia, EmergenciaSummary, Guid>
    {
        Task<bool> Panico(Guid id_taxista, string latitude, string longitude);
        Task<bool> AlterarStatus(Guid id_emergencia, StatusEmergencia status);
    }
}


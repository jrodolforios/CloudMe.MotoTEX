using CloudMe.ToDeTaxi.Domain.Enums;
using CloudMe.ToDeTaxi.Domain.Model.Taxista;
using CloudMe.ToDeTaxi.Infraestructure.Entries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Services.Abstracts
{
    public interface IEmergenciaService : IServiceBase<Emergencia, EmergenciaSummary, Guid>
    {
        Task<bool> Panico(Guid id_taxista, string latitude, string longitude);
        Task<bool> AlterarStatus(Guid id_emergencia, StatusEmergencia status);
    }
}


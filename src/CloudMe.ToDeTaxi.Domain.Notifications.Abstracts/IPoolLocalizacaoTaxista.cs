using CloudMe.ToDeTaxi.Domain.Model.Taxista;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudMe.ToDeTaxi.Domain.Notifications.Abstracts
{
    public interface IPoolLocalizacaoTaxista: IHostedService
    {
        Task SolicitarLocalizacao();
        Task EnviarPanico(EmergenciaSummary emergencia);
    }
}

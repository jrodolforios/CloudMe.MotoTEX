using CloudMe.MotoTEX.Domain.Model.Taxista;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Notifications.Abstracts
{
    public interface IPoolLocalizacaoTaxista: IHostedService
    {
        Task SolicitarLocalizacao();
        Task EnviarPanico(EmergenciaSummary emergencia);
    }
}

using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Notifications.Abstract.Proxies
{
    public interface IProxyNotificacoesLocalizacao
    {
        Task InformarLocalizacaoTaxista(Guid idTaxista, double latitude, double longitude, double angulo);
        Task InformarLocalizacaoPassageiro(Guid idTaxista, double latitude, double longitude, double angulo);
    }
}

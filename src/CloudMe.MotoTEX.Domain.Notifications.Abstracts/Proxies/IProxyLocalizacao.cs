using System.Threading.Tasks;
using System;
using CloudMe.MotoTEX.Infraestructure.EF.Contexts;
using CloudMe.MotoTEX.Domain.Model.Taxista;

namespace CloudMe.MotoTEX.Domain.Notifications.Abstract.Proxies
{
    public interface IProxyLocalizacao
    {
        Task SolicitarLocalizacaoTaxistas();
        Task SolicitarLocalizacaoPassageiros();
        Task InformarLocalizacaoTaxista(Guid idTaxista, double latitude, double longitude, double angulo);
        Task InformarLocalizacaoPassageiro(Guid idTaxista, double latitude, double longitude, double angulo);
    }
}

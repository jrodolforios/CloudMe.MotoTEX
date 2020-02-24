using CloudMe.MotoTEX.Domain.Model.Taxista;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Notifications.Abstract.Proxies
{
    public interface IProxyEmergencia
    {
        Task EnviarPanico(EmergenciaSummary emergencia);
    }
}

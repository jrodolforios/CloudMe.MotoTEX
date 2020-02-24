using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CloudMe.MotoTEX.Domain.Notifications.Compat
{
    [Authorize]
    public class HubLocalizacaoTaxista : Hub
    {
    }
}

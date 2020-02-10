using CloudMe.MotoTEX.Infraestructure.Entries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Notifications.Abstract
{
    public interface IFirebaseNotifications
    {
        Task<bool> SendPushNotification(IEnumerable<Usuario> usuarios, string title, string body, object data);
        Task<bool> SendPushNotification(GrupoUsuario grupo, string title, string body, object data);
    }
}

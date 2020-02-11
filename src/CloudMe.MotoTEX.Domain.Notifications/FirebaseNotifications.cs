using CloudMe.MotoTEX.Domain.Model.Config;
using CloudMe.MotoTEX.Domain.Model.Mensagem;
using CloudMe.MotoTEX.Domain.Notifications.Abstract;
using CloudMe.MotoTEX.Infraestructure.Entries;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Domain.Notifications
{
    public class FirebaseNotifications: IFirebaseNotifications
    {
        private readonly IConfiguration configuration;
        private FirebaseConfiguration firebaseConfig;

        public FirebaseNotifications(IConfiguration configuration)
        {
            firebaseConfig = new FirebaseConfiguration();
            configuration.GetSection("FirebaseConfiguration").Bind(firebaseConfig);
        }

        public async Task<bool> SendPushNotification(IEnumerable<Usuario> usuarios, string title, string body, object data)
        {
            var registration_ids = usuarios
                .Where(usr => !string.IsNullOrEmpty(usr.DeviceToken))
                .Select(usr => usr.DeviceToken).ToArray();

            if (registration_ids.Count() == 0) return false;

            var pushNotification = new PushNotification
            {
                notification = new Notification
                {
                    title = title,
                    text = body,
                },
                data = data,
                registration_ids = registration_ids
            };

            var jsonMessage = JsonConvert.SerializeObject(pushNotification);

            var request = new HttpRequestMessage(HttpMethod.Post, firebaseConfig.Endpoint);
            request.Headers.TryAddWithoutValidation("Authorization", "key =" + firebaseConfig.ServerKey);
            request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
            HttpResponseMessage result;
            using (var client = new HttpClient())
            {
                result = await client.SendAsync(request);
            }

            return result.IsSuccessStatusCode;
        }

        public async Task<bool> SendPushNotification(GrupoUsuario grupo, string title, string body, object data)
        {
            return await SendPushNotification(grupo.Usuarios.Select(x => x.Usuario), title, body, data);
        }
    }
}

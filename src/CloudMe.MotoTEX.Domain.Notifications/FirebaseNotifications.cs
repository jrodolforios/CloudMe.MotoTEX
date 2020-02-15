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
            var registration_ids_tx = usuarios
                .Where(usr => !string.IsNullOrEmpty(usr.DeviceToken) && usr.tipo == Enums.TipoUsuario.Taxista)
                .Select(usr => usr.DeviceToken).ToArray();

            var registration_ids_psg = usuarios
                .Where(usr => !string.IsNullOrEmpty(usr.DeviceToken) && usr.tipo == Enums.TipoUsuario.Passageiro)
                .Select(usr => usr.DeviceToken).ToArray();

            var pushNotification = new PushNotification
            {
                notification = new Notification
                {
                    title = title,
                    text = body,
                },
                data = data,
                registration_ids = null
            };

            var enviou_tx = false;
            if (registration_ids_tx.Count() > 0)
            {
                pushNotification.registration_ids = registration_ids_tx;

                var jsonMessage = JsonConvert.SerializeObject(pushNotification);

                var request = new HttpRequestMessage(HttpMethod.Post, firebaseConfig.Endpoint);
                request.Headers.TryAddWithoutValidation("Authorization", "key =" + firebaseConfig.ServerKey_Taxista);
                request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
                HttpResponseMessage result;
                using (var client = new HttpClient())
                {
                    result = await client.SendAsync(request);
                }

                enviou_tx = result.IsSuccessStatusCode;
            }

            var enviou_psg = false;
            if (registration_ids_psg.Count() > 0)
            {
                pushNotification.registration_ids = registration_ids_psg;

                var jsonMessage = JsonConvert.SerializeObject(pushNotification);

                var request = new HttpRequestMessage(HttpMethod.Post, firebaseConfig.Endpoint);
                request.Headers.TryAddWithoutValidation("Authorization", "key =" + firebaseConfig.ServerKey_Passageiro);
                request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");
                HttpResponseMessage result;
                using (var client = new HttpClient())
                {
                    result = await client.SendAsync(request);
                }

                enviou_psg = result.IsSuccessStatusCode;
            }

            return enviou_tx || enviou_psg;
        }

        public async Task<bool> SendPushNotification(GrupoUsuario grupo, string title, string body, object data)
        {
            return await SendPushNotification(grupo.Usuarios.Select(x => x.Usuario), title, body, data);
        }
    }
}

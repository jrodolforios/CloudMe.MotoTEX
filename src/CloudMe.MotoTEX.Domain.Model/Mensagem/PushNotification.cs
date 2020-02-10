using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Model.Mensagem
{
    public class Notification
    {
        public string title { get; set; }
        public string text { get; set; }
    }

    public class PushNotification
    {
        public string[] registration_ids { get; set; }
        public Notification notification { get; set; }
        public object data { get; set; }
    }
}

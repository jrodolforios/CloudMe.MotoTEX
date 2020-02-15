using System;
using System.Collections.Generic;
using System.Text;

namespace CloudMe.MotoTEX.Domain.Model.Config
{
    public class FirebaseConfiguration
    {
        public string Endpoint { get; set; }
        public string ServerKey_Taxista { get; set; }
        public string ServerKey_Passageiro { get; set; }
        public string InheritedServerKey { get; set; }
    }
}

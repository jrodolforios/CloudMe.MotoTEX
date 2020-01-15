using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudMe.MotoTEX.Api
{
    public class CorsParam
    {
        public bool allowAny { get; set; }
        public string [] allowed { get; set; }
    }

    public class CORSConfig
    {
        public CorsParam headers { get; set; }
        public CorsParam methods { get; set; }
        public CorsParam origins { get; set; }
        public bool credentials { get; set; }

        public CORSConfig() { }
    }
}

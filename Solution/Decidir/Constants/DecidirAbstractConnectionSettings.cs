using System;
using System.Collections.Generic;
using System.Text;

namespace Decidir.Constants
{
    public abstract class DecidirAbstractConnectionSettings
    {
        public virtual EAmbiente Environment { get; set; }
        public static string DecidirHttpClientFactoryClientName { get; set; } = "DecidirSDKHttpClient";
        public string PrivateApiKey { get; set; }
        public string PublicApiKey { get; set; }
        public string ValidateApiKey { get; set; } = null;
        public string Merchant { get; set; } = null;
        public string Grouper { get; set; } = "";
        public string Developer { get; set; } = "";
    }
}
